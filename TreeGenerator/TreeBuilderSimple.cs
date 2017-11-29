using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using System.Xml;


namespace TreeGenerator
{
    public struct NodeDetails
    {
        // These fields match the ones in TreeData.xsd
        // nodeID - should be a unique GUID
        // parentNodeID 
        // nodeDescription Title
        // nodeNote - Legacy, from ORG Chart
        // nodeCategory - legacy
        // nodeSOD - not sure as of 11/23/2017 but probably org chart
        // scripting - a generic field for drawing lines and other behavior I need
        
        public string nodeID;
        public string parentNodeID;
        public string nodeDescription;
        public string nodeNote;
        public string nodeCategory;
        public string nodeSOD;
        public string scripting;
        // nodetype - condition or action
        //   orphan - no parent line
        public string nodetype;
        public NodeDetails(int def)
        {
            nodeID = "1";
            parentNodeID = "";
            nodeDescription = "description";
            nodeNote = "note";
            nodeCategory = "category";
            nodeSOD = "sod";
            scripting = "scripting";
            nodetype = "nodetype";
        }
    }
    public class TreeBuilderSimple : IDisposable
    {
        System.Collections.Hashtable peopleHash = new System.Collections.Hashtable(); // Will contain consistent colors by person/category
        System.Collections.Hashtable catHash = new System.Collections.Hashtable(); // Will contain consistent colors by person/category

        System.Collections.ArrayList listOfBoxRegions = new System.Collections.ArrayList();
        System.Collections.ArrayList listOfLinesToAdd = new System.Collections.ArrayList();
        // 11/23/2017 - I switched to this so that I can more easily reference node data without attaching everything to am 
        // XML node ... instead I just put the index number into the xml node and grab the full struct details 
        List<NodeDetails> listOfNodeStructures = new List<NodeDetails>();

       public enum Placement
        {
            Bottom,Top,Middle,Left,Right
        }

        public struct lineToLine
        {
            public string source;
            public string dest;
            //i.e., goodarrow|dash
            public string linetype;
            public Placement destx;
            public Placement desty;
            public Placement sourcex;
            public Placement sourcey;
        }
        public struct Regions
        {
            public Rectangle Rect;
            public string name;
        }
      
            public enum mode { ORG, DUTY };
        public mode myMode = mode.ORG;
        #region Private Members

        private Color _FontColor = Color.Black;
        private int _BoxWidth = 120;
        private int _BoxHeight = 60;
        private int _Margin = 20;
        private int _HorizontalSpace = 30;
        private int _VerticalSpace = 30;
        private int _FontSize = 9;
        private string _FontName = "Courier New";
        private int imgWidth = 0;
        private int imgHeight = 0;
        private Graphics gr;
        private Color _LineColor = Color.Black;
        private float _LineWidth = 2;
        private Color _BoxFillColor = Color.White;
        private Color _BGColor = Color.White;
        private TreeData.TreeDataTableDataTable dtTree;
        private XmlDocument nodeTreeXmlDocument;
        double PercentageChangeX;// = ActualWidth / imgWidth;
        double PercentageChangeY;// = ActualHeight / imgHeight;
        #endregion
        private Color[] colorsToUse = new Color[10] { Color.Blue, Color.BlueViolet, Color.BurlyWood, Color.Brown, Color.Peru, Color.Green, Color.IndianRed, Color.Orchid, Color.Olive, Color.Moccasin };

        /// <summary>
        /// 
        /// </summary>
        public void SetSchema_OrgChart()
        {
            LineWidth = 6;
            FontSize = 10;
            VerticalSpace = 25;
            HorizontalSpace = 34;
            Margin = 28;
            BoxHeight = 100;
            BoxWidth = 126;
            FontColor = Color.Black;
            BoxFillColor = Color.LightYellow;
            LineColor = Color.Black;
            BGColor = Color.LightGray;


        }
        public void SetSchema_TaskChart()
        {
            LineWidth = 6;
            FontSize = 10;
            VerticalSpace = 25;
            HorizontalSpace = 34;
            Margin = 28;
            BoxHeight = 90;
            BoxWidth = 126;
            FontColor = Color.Black;
            BoxFillColor = Color.LightYellow;
            LineColor = Color.Black;
            BGColor = Color.Firebrick;


        }
        public struct BoxDetail
        {
            public Color boxfillcolor;
            public int boxthickness;
            public int gradient; // 0 no, 1 yes
            public Color gradientColor;
            public BoxDetail(int i)
            {
                boxfillcolor = Color.Purple;
                boxthickness = 6;
                gradient = 0;
                gradientColor = Color.Purple;

            }
        }
        /// <summary>
        /// A structure to hold format details, so they are more easily "found"
        /// </summary>
        public struct Format
        {
            public int secondline_thick;
            public Color secondline_color;
            public Color secondaryFontColor;
            public string secondaryFontName;
            public int secondaryFontSize;
            public BoxDetail actionbox;
            public BoxDetail outcomebox;
            public BoxDetail defaultbox;

            public Color calloutboxcolor;
            public int xmarginextra; // move it a bit to the right

            // this is how thick the border around a box is (separated from main line width)
            public int boxlinewidth;
            public Format(int k)
            {
                secondline_thick = 4;
                secondline_color = Color.Green;
                secondaryFontColor = Color.Pink;
                secondaryFontName = "Courier New";
                actionbox = new BoxDetail(1);
                outcomebox = new BoxDetail(1);
                defaultbox = new BoxDetail(1);
                secondaryFontSize = 11;
                boxlinewidth = 2;
                calloutboxcolor = Color.Green;
                xmarginextra = 0;
            }
            public int GetIsGradient(string nodetype)
            {
                if (nodetype  == "action")
                {
                    return actionbox.gradient;
                }
                if (nodetype == "default" || nodetype == "")
                {
                    return defaultbox.gradient;
                }
                if (nodetype == "outcome")
                {
                    return outcomebox.gradient;
                }
                return 0;
            }
            public Color GetGradientColor(string nodetype)
            {
                if (nodetype == "action")
                {
                    return actionbox.gradientColor;
                }
                if (nodetype == "default" || nodetype =="")
                {
                    return defaultbox.gradientColor;
                }
                if (nodetype == "outcome")
                {
                    return outcomebox.gradientColor;
                }
                return Color.Orange;
            }

            public Color GetColor(string nodetype)
            {
                if (nodetype == "default" || nodetype == "")
                {
                    return defaultbox.boxfillcolor;
                }
                if (nodetype == "action")
                {
                    return actionbox.boxfillcolor;
                  
                }
                if (nodetype == "outcome")
                {
                    return  outcomebox.boxfillcolor;
                   
                }
                return Color.Pink;
            }
            public int GetBoxSize(string nodetype)
            {
                if (nodetype == "default" || nodetype == "")
                {
                    return defaultbox.boxthickness;
                }
                if (nodetype == "action")
                {
                    return actionbox.boxthickness;
                }
                if (nodetype == "outcome")
                {
                    return outcomebox.boxthickness;
                    
                }
                return 9;
            }
        }
        public Format format = new Format(1);

        #region Public Properties
        public XmlDocument xmlTree
        {
            get
            {
                return nodeTreeXmlDocument;
            }
        }
        public string FontName
        {
            get { return _FontName; }
            set { _FontName = value; }
        }
        public Color BoxFillColor
        {
            get { return _BoxFillColor; }
            set { _BoxFillColor = value; }
        }
        public int BoxWidth
        {
            get { return _BoxWidth; }
            set { _BoxWidth = value; }
        }
        public int BoxHeight
        {
            get { return _BoxHeight; }
            set { _BoxHeight = value; }
        }
        public int Margin
        {
            get { return _Margin; }
            set { _Margin = value; }
        }
        public int HorizontalSpace
        {
            get { return _HorizontalSpace; }
            set { _HorizontalSpace = value; }
        }
        public int VerticalSpace
        {
            get { return _VerticalSpace; }
            set { _VerticalSpace = value; }
        }
        public int FontSize
        {
            get { return _FontSize; }
            set { _FontSize = value; }
        }
        public Color LineColor
        {
            get { return _LineColor; }
            set { _LineColor = value; }
        }
        public float LineWidth
        {
            get { return _LineWidth; }
            set { _LineWidth = value; }
        }


        public Color BGColor
        {
            get { return _BGColor; }
            set { _BGColor = value; }
        }

        public Color FontColor
        {
            get { return _FontColor; }
            set { _FontColor = value; }
        }

        #endregion
        #region Public Methods

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="TreeData"></param>
        public TreeBuilderSimple(TreeData.TreeDataTableDataTable TreeData)
        {
            dtTree = TreeData;

        }

        public void UpdateTable(TreeData.TreeDataTableDataTable TreeData)
        {
            dtTree = TreeData;
            listOfBoxRegions.Clear();
            listOfLinesToAdd.Clear();
        }

        public void Dispose()
        {
            dtTree = null;

            if (gr != null)
            {
                gr.Dispose();
                gr = null;
            }
        }
        /// <summary>
        /// This overloaded method can be used to return the image using it's default calculated size, without resizing
        /// </summary>
        /// <param name="StartFromNodeID"></param>
        /// <param name="ImageType"></param>
        /// <returns></returns>
        public System.IO.Stream GenerateTree(
                                        string StartFromNodeID,
                                        ImageFormat ImageType)
        {
            return GenerateTree(-1, -1, StartFromNodeID, ImageType, false, false);


        }

        private bool _showCategory = false;
        private bool _showOverridePerson = false;

        /// <summary>
        /// Creates the tree
        /// </summary>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <param name="StartFromNodeID"></param>
        /// <param name="ImageType"></param>
        /// <returns></returns>
        public System.IO.Stream GenerateTree(int Width,
                                        int Height,
                                        string StartFromNodeID,
                                        ImageFormat ImageType, bool ShowCategory, bool ShowOverridePerson)
        {
            MemoryStream Result = new MemoryStream();
            _showCategory = ShowCategory;
            _showOverridePerson = ShowOverridePerson;


            //reset image size
            imgHeight = 0;
            imgWidth = 0;
            //reset percentage change
            PercentageChangeX = 1.0;
            PercentageChangeY = 1.0;
            //define the image
            nodeTreeXmlDocument = null;
            nodeTreeXmlDocument = new XmlDocument();
            string rootDescription = string.Empty;
            string rootNote = string.Empty;
            string rootCategory = string.Empty;
            string rootSOD = string.Empty;

            if (dtTree == null) return null;
            NodeDetails nodeDetails = new NodeDetails(1);

            if (dtTree.Select(string.Format("nodeID='{0}'", StartFromNodeID)).Length > 0)
            {
                nodeDetails.nodeID = StartFromNodeID;
                nodeDetails.nodeDescription = ((TreeData.TreeDataTableRow)dtTree.Select(string.Format("nodeID='{0}'", StartFromNodeID))[0]).nodeDescription;
                nodeDetails.nodeNote = ((TreeData.TreeDataTableRow)dtTree.Select(string.Format("nodeID='{0}'", StartFromNodeID))[0]).nodeNote;
                nodeDetails.nodeCategory = ((TreeData.TreeDataTableRow)dtTree.Select(string.Format("nodeID='{0}'", StartFromNodeID))[0]).nodeCategory;
                nodeDetails.nodeSOD = ((TreeData.TreeDataTableRow)dtTree.Select(string.Format("nodeID='{0}'", StartFromNodeID))[0]).nodeSOD;
                nodeDetails.scripting = ((TreeData.TreeDataTableRow)dtTree.Select(string.Format("nodeID='{0}'", StartFromNodeID))[0]).scripting;
                nodeDetails.nodetype = ((TreeData.TreeDataTableRow)dtTree.Select(string.Format("nodeID='{0}'", StartFromNodeID))[0]).nodetype                   ;
            }
            listOfNodeStructures.Clear();

            // This does get called multiple times because of all the Value bindings on the UI elements
            // especially at start there's a whole bunch of near recursive calls, worth looking into

            XmlNode RootNode = GetXMLNode(nodeDetails, -1);
            nodeTreeXmlDocument.AppendChild(RootNode);
            BuildTree(RootNode, 0);

            //check for intersection. line below should be remarked if not debugging
            //as it affects performance measurably.
            //OverlapExists();
            Bitmap bmp = new Bitmap(imgWidth, imgHeight);
            gr = Graphics.FromImage(bmp);
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality; // seems same as AntiAlias
            gr.Clear(_BGColor);
            DrawChart(RootNode);

            foreach (lineToLine liner in listOfLinesToAdd)
            {
                Rectangle source = new Rectangle(0, 0, 0, 0);
                Rectangle dest = new Rectangle(0, 0, 0, 0);
                foreach (Regions region in listOfBoxRegions)
                {
                    if (region.name.Trim() == liner.source)
                    {
                        source = region.Rect;
                    }
                    else
                        if (region.name.Trim() == liner.dest)
                    {
                        dest = region.Rect;
                    }
                }

                //build linetype based on strings
               // string[] ltypes = liner.linetype.Split(new char[1] {',' }, StringSplitOptions.RemoveEmptyEntries);
                linetypes mylinetypetopass = linetypes.dash;
                string s = liner.linetype.Replace("|", ", "); // need to convert | into , for the .parse
                mylinetypetopass = (linetypes) Enum.Parse(typeof(linetypes),s);
               // foreach (string s in ltypes)
              //  {
                //    if (s == "goodarrow")
              //          mylinetypetopass = linetypes.goodarrow;
              //  }

                Pen extraPen = new Pen(format.secondline_color, format.secondline_thick);
                // draw an extra line
                int destlinex = dest.Left;
                int destliney = dest.Top;
                int sourcelinex = dest.Right;
                int sourceliney = dest.Bottom;
                
                switch (liner.destx)
                {
                    case Placement.Right: destlinex = dest.Right; break;
                    case Placement.Left: destlinex = dest.Left; break;
                    case Placement.Middle: destlinex = ((dest.Right - dest.Left) / 2) + dest.Left ; break;
                }
                switch (liner.desty)
                {
                    case Placement.Top: destliney = dest.Top; break;
                    case Placement.Bottom: destliney = dest.Bottom; break;
                    case Placement.Middle: destliney = ((dest.Bottom - dest.Top) / 2) + dest.Top;  break;
                }
                switch (liner.sourcex)
                {
                    case Placement.Right: sourcelinex = source.Right; break;
                    case Placement.Left: sourcelinex = source.Left; break;
                    case Placement.Middle: sourcelinex = ((source.Right - source.Left)/2)+source.Left; break;
                }
                switch (liner.sourcey)
                {
                    case Placement.Top: sourceliney = source.Top; break;
                    case Placement.Bottom: sourceliney = source.Bottom; break;
                    case Placement.Middle: sourceliney = ((source.Bottom - source.Top) / 2) + source.Top;  break;
                }

                DrawLine(extraPen, sourcelinex,
                                       sourceliney,
                                       destlinex,
                                       destliney, mylinetypetopass);


              

            }


            //if caller does not care about size, use original calculated size
            if (Width < 0)
            {
                Width = imgWidth;
            }
            if (Height < 0)
            {
                Height = imgHeight;
            }

            Bitmap ResizedBMP = new Bitmap(bmp, new Size(Width, Height));

            /*
            string watermarkText = DateTime.Now.ToString();
            // Use this to watermark the bottom-left corner
            SizeF measuredSize = gr.MeasureString(watermarkText, drawFont); ;
            //ResizedBMP.Height - measuredSize.Height
            gr.DrawString(watermarkText, drawFont, new SolidBrush(Color.Black), 200, 200);
            */

            //after resize, determine the change percentage
            PercentageChangeX = Convert.ToDouble(Width) / imgWidth;
            PercentageChangeY = Convert.ToDouble(Height) / imgHeight;
            //after resize - change the coordinates of the list, in order return the proper coordinates
            //for each node
            if (PercentageChangeX != 1.0 || PercentageChangeY != 1.0)
            {
                //only resize coordinates if there was a resize
                CalculateImageMapData();
            }

          


            ResizedBMP.Save(Result, ImageType);
            ResizedBMP.Dispose();
            bmp.Dispose();
            gr.Dispose();
            return Result;


        }

        
        [Flags]
        enum linetypes
        {
            none = 0,
            empty = 1,
            goodarrow = 2,
            round = 4,
            dash = 8,
            twocolor =16
        }
        /// <summary>
        /// Wrapper so I can add options for arrowheads, etc, basedo n settings
        /// </summary>
        /// <param name="pen"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        private void DrawLine(Pen pen, int x, int y, int x2, int y2, linetypes style)
        {
            if (style.HasFlag(linetypes.goodarrow))
            {
                AdjustableArrowCap myArrow = new AdjustableArrowCap(5, 5, false);
                pen.CustomEndCap = myArrow;
            }
            if (style.HasFlag(linetypes.round))
            {
                pen.StartCap = LineCap.RoundAnchor;
            }
            if (style.HasFlag(linetypes.twocolor))
            {
                AdjustableArrowCap myArrow = new AdjustableArrowCap(2,2, false);
                Pen p2 = new Pen(Color.Black);
                p2.StartCap = LineCap.RoundAnchor;
                
                p2.CustomEndCap = myArrow;
               
                p2.Width = pen.Width*3;

                int ytilt = 0;
                int xtilt = 0;
                if (x > x2) xtilt = xtilt * -1;
                if (y < y2) ytilt = ytilt * -1;
                gr.DrawLine(p2, x, y, x2 + xtilt, y2 + ytilt);

            }
            if(style.HasFlag(linetypes.dash))
            {
                pen.DashStyle = DashStyle.DashDotDot;
            }

            gr.DrawLine(pen, x,   y,    x2,  y2);

            
            // draw arrow head
            //gr.DrawLine(pen, x2, y2, x2 + 8, y2 - 8);
            //gr.DrawLine(pen, x2, y2, x2 - 8, y2 + 8);
        }
        /// <summary>
        /// the node holds the x,y in attributes
        /// use them to calculate the position
        /// This is public so it can be used by other classes trying to calculate the 
        /// cursor/mouse location
        /// </summary>
        /// <param name="oNode"></param>
        /// <returns></returns>
        public Rectangle getRectangleFromNode(XmlNode oNode)
        {
            //  if (oNode == null) return new Rectangle(0, 0, 0, 0);

            if (oNode.Attributes["X"] == null || oNode.Attributes["Y"] == null)
            {
                throw new Exception("Both attributes X,Y must exist for node.");
            }
            int X = Convert.ToInt32(oNode.Attributes["X"].InnerText);
            int Y = Convert.ToInt32(oNode.Attributes["Y"].InnerText);

            Rectangle Result = new Rectangle(X, Y, (int)(_BoxWidth * PercentageChangeX), (int)(_BoxHeight * PercentageChangeY));
            return Result;

        }
        #endregion
        #region Private Methods
        /// <summary>
        /// convert the datatable to an XML document
        /// </summary>
        /// <param name="oNode"></param>
        /// <param name="y">its the node level nothing for parent, 0 for first child row, etc.</param>
        private void BuildTree(XmlNode oNode, int y)
        {
            XmlNode childNode = null;
            //has children
            foreach (TreeData.TreeDataTableRow childRow in dtTree.Select(
                string.Format("parentNodeID='{0}'", oNode.Attributes["nodeID"].InnerText)))
            {
                NodeDetails nodeDetails = new NodeDetails(1);
                nodeDetails.nodeID = childRow.nodeID;
                nodeDetails.nodeDescription = childRow.nodeDescription;
                nodeDetails.nodeNote = childRow.nodeNote;
                nodeDetails.nodeCategory = childRow.nodeCategory;
                nodeDetails.nodeSOD = childRow.nodeSOD;
                nodeDetails.scripting = childRow.scripting;
                nodeDetails.nodetype = childRow.nodetype;
                //for each child node call this function again
                childNode = GetXMLNode(nodeDetails, y);
                oNode.AppendChild(childNode);
                BuildTree(childNode, y + 1);

            }
            //build node data
            //after checking for nodes we can add the current node
            int StartX;
            int StartY;
            int xmargin = format.xmarginextra;

            int[] ResultsArr = new int[] {GetXPosByOwnChildren(oNode),
                                    GetXPosByParentPreviousSibling(oNode),
                                    GetXPosByPreviousSibling(oNode),
                                    _Margin,xmargin };
            Array.Sort(ResultsArr);

            StartX = ResultsArr[4];
            StartY = (y * (_BoxHeight + _VerticalSpace)) + _Margin;
            int width = _BoxWidth;
            int height = _BoxHeight;
            //update the coordinates of this box into the matrix, for later calculations
            oNode.Attributes["X"].InnerText = StartX.ToString();
            oNode.Attributes["Y"].InnerText = StartY.ToString();

            //update the image size
            if (imgWidth < (StartX + width + _Margin + xmargin))
            {
                imgWidth = StartX + width + _Margin + xmargin;
            }
            if (imgHeight < (StartY + height + _Margin))
            {
                  imgHeight = StartY + height + _Margin;
               
            }





        }

        /************************************************************************************************************************
         * The box position is affected by:
         * 1. The previous sibling (box on the same level)
         * 2. The positions of it's children
         * 3. The position of it's uncle (parents' previous sibling)/ cousins (parents' previous sibling children)
         * What determines the position is the farthest x of all the above. If all/some of the above have no value, the margin 
         * becomes the dtermining factor.
         * **********************************************************************************************************************
        */

        private int GetXPosByPreviousSibling(XmlNode CurrentNode)
        {
            int Result = -1;
            int X = -1;
            XmlNode PrevSibling = CurrentNode.PreviousSibling;
            if (PrevSibling != null)
            {
                if (PrevSibling.HasChildNodes)
                {

                    //Result = Convert.ToInt32(PrevSibling.LastChild.Attributes["X"].InnerText ) + _BoxWidth + _HorizontalSpace;
                    //need to loop through all children for all generations of previous sibling
                    X = Convert.ToInt32(GetMaxXOfDescendants(PrevSibling.LastChild));
                    Result = X + _BoxWidth + _HorizontalSpace;

                }
                else
                {

                    Result = Convert.ToInt32(PrevSibling.Attributes["X"].InnerText) + _BoxWidth + _HorizontalSpace;
                }
            }
            return Result;
        }

        private int GetXPosByOwnChildren(XmlNode CurrentNode)
        {
            int Result = -1;

            if (CurrentNode.HasChildNodes)
            {
                int lastChildX = Convert.ToInt32(CurrentNode.LastChild.Attributes["X"].InnerText);
                int firstChildX = Convert.ToInt32(CurrentNode.FirstChild.Attributes["X"].InnerText);
                Result = (((lastChildX + _BoxWidth) - firstChildX) / 2) - (_BoxWidth / 2) + firstChildX;


            }
            return Result;
        }
        private int GetXPosByParentPreviousSibling(XmlNode CurrentNode)
        {
            int Result = -1;
            int X = -1;
            XmlNode ParentPrevSibling = CurrentNode.ParentNode.PreviousSibling;

            if (ParentPrevSibling != null)
            {
                if (ParentPrevSibling.HasChildNodes)
                {

                    //X = Convert.ToInt32(ParentPrevSibling.LastChild.Attributes["X"].InnerText);
                    X = GetMaxXOfDescendants(ParentPrevSibling.LastChild);
                    Result = X + _BoxWidth + _HorizontalSpace;
                }
                else
                {

                    X = Convert.ToInt32(ParentPrevSibling.Attributes["X"].InnerText);
                    Result = X + _BoxWidth + _HorizontalSpace;
                }
            }
            else //ParentPrevSibling == null
            {

                if (CurrentNode.ParentNode.Name != "#document")
                {
                    Result = GetXPosByParentPreviousSibling(CurrentNode.ParentNode);
                }
            }
            return Result;
        }
        /// <summary>
        /// Get the maximum x of the lowest child on the current tree of nodes
        /// Recursion does not work here, so we'll use a loop to climb down the tree
        /// Recursion is not a solution because we need to return the value of the last leaf of the tree.
        /// That would require managing a global variable.
        /// </summary>
        /// <param name="CurrentNode"></param>
        /// <returns></returns>
        private int GetMaxXOfDescendants(XmlNode CurrentNode)
        {
            int Result = -1;

            while (CurrentNode.HasChildNodes)
            {
                CurrentNode = CurrentNode.LastChild;

            }

            Result = Convert.ToInt32(CurrentNode.Attributes["X"].InnerText);

            return Result;
            //int Result = -1;
            //if (CurrentNode.HasChildNodes)
            //{
            //    GetMaxXOfDescendants(CurrentNode.LastChild);
            //}
            //else
            //{
            //    Result = Convert.ToInt32(CurrentNode.Attributes["X"].InnerText);
            //}
            //return Result;
        }
        
        /// <summary>
        /// create an xml node based on supplied data
        /// </summary>
        /// <returns></returns>
        private XmlNode GetXMLNode(NodeDetails nodeDetails, int level)
        {
             listOfNodeStructures.Add(nodeDetails);
            //build the node
            XmlNode resultNode = nodeTreeXmlDocument.CreateElement("Node");
            XmlAttribute attNodeID = nodeTreeXmlDocument.CreateAttribute("nodeID");

            XmlAttribute attNodeDescription = nodeTreeXmlDocument.CreateAttribute("nodeDescription");
            XmlAttribute attNodeNote = nodeTreeXmlDocument.CreateAttribute("nodeNote");
            XmlAttribute attNodeCategory = nodeTreeXmlDocument.CreateAttribute("nodeCategory");
            XmlAttribute attNodeSOD = nodeTreeXmlDocument.CreateAttribute("nodeSOD");
            XmlAttribute attStartX = nodeTreeXmlDocument.CreateAttribute("X");
            XmlAttribute attStartY = nodeTreeXmlDocument.CreateAttribute("Y");
            XmlAttribute attlevel = nodeTreeXmlDocument.CreateAttribute("level");
            XmlAttribute attStruct = nodeTreeXmlDocument.CreateAttribute("nodedata");

            //set the values of what we know
            attNodeID.InnerText = nodeDetails.nodeID;

            attNodeDescription.InnerText = nodeDetails.nodeDescription;
            attNodeNote.InnerText = nodeDetails.nodeNote;
            attNodeCategory.InnerText = nodeDetails.nodeCategory;
            attNodeSOD.InnerText = nodeDetails.nodeSOD;
            attStartX.InnerText = "0";
            attStartY.InnerText = "0";
            attStruct.InnerText = (listOfNodeStructures.Count -1).ToString();

            attlevel.InnerText = level.ToString();

            resultNode.Attributes.Append(attNodeID);

            resultNode.Attributes.Append(attNodeDescription);
            resultNode.Attributes.Append(attNodeNote);
            resultNode.Attributes.Append(attNodeCategory);
            resultNode.Attributes.Append(attNodeSOD);
            resultNode.Attributes.Append(attStartX);
            resultNode.Attributes.Append(attStartY);
            resultNode.Attributes.Append(attlevel);
            resultNode.Attributes.Append(attStruct);

            return resultNode;

        }

        private Color GetNextColor(System.Collections.Hashtable hashy)
        {
            return colorsToUse[hashy.Count];
        }



        Font drawFont = null; SolidBrush drawBrush = null;
        /// <summary>
        /// Draws the actual chart image.
        /// </summary>
        private void DrawChart(XmlNode oNode)
        {
            /*
            ---------------------------------------------------------------
            Setup
            ---------------------------------------------------------------
            */
            // Create font and brush.
            //
             drawFont = new Font(_FontName, _FontSize);
        
            
            Font secondFont = new Font(format.secondaryFontName, format.secondaryFontSize);

            drawBrush = new SolidBrush(_FontColor);
            SolidBrush drawSecondFontBrush = new SolidBrush(format.secondaryFontColor);
            SolidBrush drawBrushError = new SolidBrush(Color.Red);

            Pen boxPen = new Pen(_LineColor, _LineWidth);


            StringFormat drawFormatheading = new StringFormat();
            drawFormatheading.Alignment = StringAlignment.Center;
            drawFormatheading.LineAlignment = StringAlignment.Near;
            

            StringFormat drawFormatSecond = new StringFormat();
            drawFormatSecond.Alignment = StringAlignment.Near;
            drawFormatSecond.LineAlignment = StringAlignment.Far;
            //find children

            NodeDetails thisNodeDetails = listOfNodeStructures[Int32.Parse(oNode.Attributes["nodedata"].InnerText)];
           
            /*
             ---------------------------------------------------------------
             Action
             ---------------------------------------------------------------
             */

            Rectangle currentRectangle = getRectangleFromNode(oNode);

            String drawString = thisNodeDetails.nodeDescription;
            String secondString = thisNodeDetails.nodeNote;
            //oNode.Attributes["nodeDescription"].InnerText +
            //    Environment.NewLine +
            //    oNode.Attributes["nodeNote"].InnerText;

           // String drawStringCategory = oNode.Attributes["nodeCategory"].InnerText;
           // String drawStringSOD = oNode.Attributes["nodeSOD"].InnerText;
           // currentRectangle = new Rectangle(currentRectangle.X + format.xmarginextra, currentRectangle.Y,
           //              currentRectangle.Width, currentRectangle.Height);
            // Adjust height of the boxes
            Rectangle oldRectangle = currentRectangle;
            
            {

                Color colorToUse = format.defaultbox.boxfillcolor;// _BoxFillColor;
                int boxSizeTouse = format.defaultbox.boxthickness;
                colorToUse = format.GetColor(thisNodeDetails.nodetype);
                boxSizeTouse = format.GetBoxSize(thisNodeDetails.nodetype);
               

                // ------------------------------------------
                // Draw existing box
                // ------------------------------------------
                if (boxSizeTouse > 0)
                {
                    Pen boxPen2 = new Pen(_LineColor, boxSizeTouse);
                    
                    gr.DrawRectangle(boxPen2, currentRectangle);
                }

                // x and y are actually screen locations 
                int mylevel = Int32.Parse(oNode.Attributes["level"].Value);
                

               
               



                
                if (false)
                {
                    if (mylevel < 2)
                    {
                        // colorToUse = Color.Pink;
                        if (drawString.IndexOf("PC BROWSER") == -1)
                            drawFont = new Font(drawFont, FontStyle.Bold);
                    }
                    if (drawString.IndexOf("SEASON 1") > -1)
                    {
                        _BoxFillColor = Color.BlanchedAlmond;
                        colorToUse = _BoxFillColor;
                    }
                    if (drawString.IndexOf("SEASON 2") > -1)
                    {
                        _BoxFillColor = Color.BurlyWood;
                        colorToUse = _BoxFillColor;

                    }
                    if (drawString.IndexOf("SEASON 3") > -1)
                    {
                        _BoxFillColor = Color.CadetBlue;
                        colorToUse = _BoxFillColor;

                    }
                }

                // ------
                // Fill the rectangle
                // ------
                
                SolidBrush defaultBrush = new SolidBrush(colorToUse);
                Brush brushToUse = defaultBrush;
                if (format.GetIsGradient(thisNodeDetails.nodetype) == 1)
                {
                    LinearGradientBrush linGrBrush = new LinearGradientBrush(
                        new Point(currentRectangle.X, currentRectangle.Y),
                        new Point(currentRectangle.X + 200, currentRectangle.Y),
                        colorToUse,   // Opaque red
                        format.GetGradientColor(thisNodeDetails.nodetype));  // Opaque blue

                    brushToUse = linGrBrush;
                }
           

                gr.FillRectangle(brushToUse, currentRectangle);


            }

           

            // ------
            // Write string
            // ------


            // Create string to draw.
            // because we escape apostophes out
            drawString = drawString.Replace("#", "'");

            // Draw string to screen.
            //if (drawString.IndexOf("No person") > -1)
            //{
            //     gr.DrawString(drawString, drawFont, drawBrushError, currentRectangle, drawFormat);
            // }
            // else
            drawTheString(drawString, drawFont, drawBrush, currentRectangle, drawFormatheading);
            // using this for more than ORG so got rid of the exception 9/27/2017
            //TODO : how to measure propr
            Rectangle secondRectangle = new Rectangle(currentRectangle.X + 10, currentRectangle.Y, currentRectangle.Width
                , currentRectangle.Height);
            gr.DrawString(secondString, secondFont, drawSecondFontBrush, currentRectangle, drawFormatSecond);





            // ------------------------------------------
            // Setup scripting actions
            // ------------------------------------------
            if (thisNodeDetails.scripting != "scripting" && thisNodeDetails.scripting != "")
            {
                // parse script
                string[] commands = thisNodeDetails.scripting.Split(new char[1] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < commands.Length; i++)
                {
                    // process each command
                    // line(<me>,<you>);
                    ProcessScripting(commands[i], thisNodeDetails.nodeID, currentRectangle);
                }
            }







            currentRectangle = oldRectangle;
            //draw connecting lines
            Regions thisRegion = new Regions();
            thisRegion.name = thisNodeDetails.nodeID;// drawString.ToLower(); chganging to ID vs name
            thisRegion.Rect = currentRectangle;
            listOfBoxRegions.Add(thisRegion);

            if (thisNodeDetails.nodetype != "orphan")
            {
                if (oNode.ParentNode.Name != "#document")
                {
                    //all but the top box should have lines growing out of their top
                    gr.DrawLine(boxPen, currentRectangle.Left + (_BoxWidth / 2),
                                                currentRectangle.Top,
                                                currentRectangle.Left + (_BoxWidth / 2),
                                                currentRectangle.Top - (_VerticalSpace / 2));
                }
            }
            if (oNode.HasChildNodes)
            {
                //all nodes which have nodes should have lines coming from bottom down
                gr.DrawLine(boxPen, currentRectangle.Left + (_BoxWidth / 2),
                                    currentRectangle.Top + _BoxHeight,
                                    currentRectangle.Left + (_BoxWidth / 2),
                                    currentRectangle.Top + _BoxHeight + (_VerticalSpace / 2));

            }
            if (oNode.PreviousSibling != null)
            {
                //the prev node has the same boss - connect the 2 nodes
                gr.DrawLine(boxPen, getRectangleFromNode(oNode.PreviousSibling).Left + (_BoxWidth / 2) - (_LineWidth / 2),
                                    getRectangleFromNode(oNode.PreviousSibling).Top - (_VerticalSpace / 2),
                                    currentRectangle.Left + (_BoxWidth / 2) + (_LineWidth / 2),
                                    currentRectangle.Top - (_VerticalSpace / 2));


            }

          
            foreach (XmlNode childNode in oNode.ChildNodes)
            {
                DrawChart(childNode);
            }
        }

        /// <summary>
        /// Creating a wrapper because I want to draw each line as a separate line?
        /// </summary>
        /// <param name="drawString"></param>
        /// <param name="drawFont"></param>
        /// <param name="drawBrush"></param>
        /// <param name="currentRectangle"></param>
        /// <param name="drawFormatheading"></param>
        private void drawTheString(string drawString, Font drawFont, SolidBrush drawBrush, Rectangle currentRectangle, StringFormat drawFormatheading)
        {
            drawString = drawString.Replace(Environment.NewLine, "&en&");
            string[] lines = drawString.Split(new string[1] {"&en&"}, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in lines)
            {
                gr.DrawString(s, drawFont, drawBrush, currentRectangle, drawFormatheading);
                int adjHeight = 0;
                //drawFont.Height.ToString()
                adjHeight = (drawFont.Height / 2) + (drawFont.Height / 4);


                currentRectangle = new Rectangle(currentRectangle.X, currentRectangle.Y + adjHeight, currentRectangle.Width,
                    currentRectangle.Height);
            }
           

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        /// <param name="me"></param>
        private void ProcessScripting(string v, string me, Rectangle currentRectangle)
        {
            int pos1 = v.IndexOf("(");
            if (pos1 > 0)
            {
                string command = v.Substring(0, pos1);

                command = command.ToLower().Trim();

                 List<string> myparams = new List<string>();

           
                int pos2 = v.IndexOf(")");
                if (pos2 > pos1)
                {
                    string tmps = v.Substring(pos1 + 1, pos2 - pos1 - 1);

                    // we have a closing bracket so let's look for comma delimitted parameters    
                    string[] tmpa = tmps.Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    //if (tmpa.Length == 1)
                   // {
                   //     param1 = tmpa[0];
                   // }
                    if (tmpa.Length > 0)
                    {
                        foreach (string s in tmpa)
                        {
                            myparams.Add(s.Trim());
                        }
                        
                    }
                    
                    //TODO a parameter array?
                }
                

                ////
                //  LINES
                ///
                if (command == "line")
                {
                    lineToLine liner = new lineToLine();
                    liner.source = me; // a number id

                    liner.dest = myparams[0];
                    string linetype = linetypes.goodarrow.ToString();

                    if (myparams.Count > 1)
                    {
                        linetype = myparams[1];
                    }
                    liner.destx = Placement.Right;
                    liner.desty = Placement.Bottom;
                    if (myparams.Count > 5)
                    {
                        Placement thex = 0;
                        Placement they = 0;
                        for (int i = 0; i < 2; i++)
                        {
                            if (myparams[2+(i*2)] == "right") thex = Placement.Right;
                            if (myparams[2 + (i * 2)] == "left") thex = Placement.Left;
                            if (myparams[2 + (i * 2)] == "middle") thex = Placement.Middle;
                            if (myparams[3 + (i * 2)] == "middle") they = Placement.Middle;
                            if (myparams[3 + (i * 2)] == "top") they = Placement.Top;
                            if (myparams[3 + (i * 2)] == "bottom") they = Placement.Bottom;

                            if (i == 0)
                            {
                                liner.sourcex = thex;
                                liner.sourcey = they;
                            }
                            if (i == 1)
                            {
                                liner.destx = thex;
                                liner.desty = they;
                            }
                        }
                       
                        

                    }
                    liner.linetype = linetype;
                    listOfLinesToAdd.Add(liner);
                }
                else
                if (command == "calloutbox")
                {
                    // draw secondary box 1 - Category - if necessary
                   // SolidBrush drawBrush_Category = new SolidBrush(Color.Black);
                    SolidBrush fillBrush_Category = new SolidBrush(Color.White);
                    // draw secondary box 2 - Person - if necessary
                    Pen boxPen_CATEGORY = new Pen(format.secondaryFontColor, 2);
                    Font drawFont_CATEGORY = new Font(format.secondaryFontName, format.secondaryFontSize);

                    StringFormat drawFormatheading = new StringFormat();
                    drawFormatheading.Alignment = StringAlignment.Center;
                    drawFormatheading.LineAlignment = StringAlignment.Near;

                    int nParam2 = 70;
                    int nParam3 = 20;

                    if (Int32.TryParse(myparams[1], out nParam2))
                    {
                       
                    }
                    if (Int32.TryParse(myparams[2], out nParam3))
                    {

                    }
                    if (nParam2 == 0) nParam2 = 70;
                    if (nParam3 == 0) nParam3 = 20;

                    int myposition = 0;
                    if (myparams.Count >= 4)
                        Int32.TryParse(myparams[3], out myposition);
                    Color toUse = Color.Black;

                        toUse = format.calloutboxcolor;// GetColorByIDX(drawStringCategory, catHash);
                        boxPen_CATEGORY = new Pen(toUse, 4);
                        //currentRectangle = 
                        DrawSecondaryBox(new SolidBrush(toUse), fillBrush_Category, boxPen_CATEGORY, drawFont_CATEGORY, 
                            drawFormatheading, currentRectangle.Location.X,
                        currentRectangle.Y, currentRectangle, myparams[0], new Size(nParam2, nParam3), myposition);


                }
                else if (command == "addtomarginleft")
                {
                    if (myparams.Count >0)
                    {
                        Int32.TryParse(myparams[0], out format.xmarginextra);
                    }
                }
                else if (command == "text")
                {
                    if (myparams.Count > 1)
                    {
                        StringFormat drawFormatheading = new StringFormat();
                        drawFormatheading.Alignment = StringAlignment.Center;
                        drawFormatheading.LineAlignment = StringAlignment.Near;

                        string texttouse = myparams[0];
                        Color c = Color.Green;
                        int c1 = 0;
                        Int32.TryParse(myparams[1], out c1);
                        c = Color.FromArgb(c1);
                        SolidBrush b = new SolidBrush(c);
                        if (texttouse.IndexOf("date") > -1)
                        {
                            texttouse = texttouse.Replace("date", DateTime.Now.ToShortDateString());
                        }
                        SizeF measuredSize = gr.MeasureString(texttouse, drawFont);
                        gr.DrawString(texttouse, drawFont, b, new PointF(gr.VisibleClipBounds.Width - measuredSize.Width, gr.VisibleClipBounds.Height - measuredSize.Height), drawFormatheading);
                    }
                }
                else if (command == "image")
                {
                    if (myparams.Count > 5)
                    {
                        // alpha
                        string salpha = myparams[5];
                        float alpha = 1;
                        float.TryParse(salpha, out alpha);
                        ColorMatrix cm = new ColorMatrix();
                        cm.Matrix33 = alpha;
                        ImageAttributes ia = new ImageAttributes();
                        ia.SetColorMatrix(cm);
                        Image inn=null;
                        int x,y,x1,y1 = 0;
                        Int32.TryParse(myparams[1],out x);
                        Int32.TryParse(myparams[2], out y);
                        Int32.TryParse(myparams[3], out x1);
                        Int32.TryParse(myparams[4], out y1);


                        string file = myparams[0];
                        if (File.Exists(file) == true)
                            inn = Image.FromFile(file);
                        float scale = 1;

                        if (myparams.Count > 6)
                        {
                            float.TryParse(myparams[6], out scale);
                        }
                        if (inn!=null)
                            gr.DrawImage(inn, new Rectangle(x, y, x1*(int)scale, y1*(int)scale), 0, 0, inn.Width, inn.Height, GraphicsUnit.Pixel, ia);
                    }
                }


            }
           
        }

        private Color GetColorByIDX(string personLookingAt, System.Collections.Hashtable hashy)
        {
            Color toUse;
            if (hashy.ContainsKey(personLookingAt))
            {
                toUse = (Color)hashy[personLookingAt];
            }
            else
            {
                // get a new color
                toUse = GetNextColor(hashy);
                hashy[personLookingAt] = toUse;

            }

            return toUse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="drawFormat"></param>
        /// <param name="currentRectangle"></param>
        /// <returns></returns>
        private Rectangle DrawSecondaryBox(SolidBrush drawBrush_Category, SolidBrush fillBrush_Category, Pen boxPen_CATEGORY, Font drawFont_CATEGORY,
            StringFormat drawFormat, int Horiz, int Vertical, Rectangle currentRectangle, string name, Size newSize, int position)
        {
            if (name == String.Empty) return currentRectangle;

            // positions
            // 0 = lower left (default)
            // 1 =  upper right

            Rectangle miniRect = new Rectangle(new Point(Horiz + currentRectangle.Width - newSize.Width, Vertical + currentRectangle.Height - (newSize.Height/2)), newSize);
            if (position != 0)
            {
                if (position == 1)
                {
                    miniRect = new Rectangle(new Point(Horiz ,
                             Vertical ),
                            newSize);
                }
            }
            gr.DrawRectangle(boxPen_CATEGORY, miniRect);
            gr.FillRectangle(fillBrush_Category, miniRect);
            gr.DrawString(name, drawFont_CATEGORY, drawBrush_Category, miniRect, drawFormat);
            return currentRectangle;
        }

        /// <summary>
        /// After resizing the image, all positions of the rectanlges need to be 
        /// recalculated too.
        /// </summary>
        /// <param name="ActualWidth"></param>
        /// <param name="ActualHeight"></param>
        private void CalculateImageMapData()
        {

            int X = 0;
            int newX = 0;
            int Y = 0;
            int newY = 0;
            foreach (XmlNode oNode in nodeTreeXmlDocument.SelectNodes("//Node"))
            {
                //go through all nodes and resize the coordinates
                X = Convert.ToInt32(oNode.Attributes["X"].InnerText);
                Y = Convert.ToInt32(oNode.Attributes["Y"].InnerText);
                newX = (int)(X * PercentageChangeX);
                newY = (int)(Y * PercentageChangeY);
                oNode.Attributes["X"].InnerText = newX.ToString();
                oNode.Attributes["Y"].InnerText = newY.ToString();

            }

        }
        /// <summary>
        /// used for testing purposes, to see if overlap exists between at least 2 boxes.
        /// </summary>
        /// <returns></returns>
        private bool OverlapExists()
        {

            List<Rectangle> listOfRectangles = new List<Rectangle>(); //the list of all objects
            int X;
            int Y;
            Rectangle currentRect;
            foreach (XmlNode oNode in nodeTreeXmlDocument.SelectNodes("//Node"))
            {
                //go through all nodes and resize the coordinates
                X = Convert.ToInt32(oNode.Attributes["X"].InnerText);
                Y = Convert.ToInt32(oNode.Attributes["Y"].InnerText);
                currentRect = new Rectangle(X, Y, _BoxWidth, _BoxHeight);
                //before adding the node we check if the space it is supposed to occupy is already occupied.
                foreach (Rectangle rect in listOfRectangles)
                {
                    if (currentRect.IntersectsWith(rect))
                    {
                        //problem
                        return true;

                    }


                }
                listOfRectangles.Add(currentRect);

            }
            return false;
        }


        #endregion


    }
}
