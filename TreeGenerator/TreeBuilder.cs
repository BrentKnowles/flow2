#region Copyright © 2007 Rotem Sapir
/*
 * This software is provided 'as-is', without any express or implied warranty.
 * In no event will the authors be held liable for any damages arising from the
 * use of this software.
 *
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, subject to the following restrictions:
 *
 * 1. The origin of this software must not be misrepresented; you must not claim
 * that you wrote the original software. If you use this software in a product,
 * an acknowledgment in the product documentation is required, as shown here:
 *
 * Portions Copyright © 2007 Rotem Sapir
 *
 * 2. No substantial portion of the source code of this library may be redistributed
 * without the express written permission of the copyright holders, where
 * "substantial" is defined as enough code to be recognizably from this library.
*/
#endregion
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
   
    public class TreeBuilder : IDisposable
    {
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
        private XmlDocument nodeTree;
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

        #region Public Properties
        public XmlDocument xmlTree
        {
            get 
            {
                return nodeTree;
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
        public TreeBuilder(TreeData.TreeDataTableDataTable TreeData)
        {
            dtTree = TreeData;
          
        }

        public void UpdateTable(TreeData.TreeDataTableDataTable TreeData)
        {
            dtTree = TreeData;
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
            nodeTree = null;
            nodeTree = new XmlDocument();
            string rootDescription=string.Empty;
            string rootNote = string.Empty;
            string rootCategory = string.Empty;
            string rootSOD = string.Empty;

            if (dtTree == null) return null;

            if(dtTree.Select(string.Format("nodeID='{0}'",StartFromNodeID)).Length>0)
            {
                rootDescription = ((TreeData.TreeDataTableRow) dtTree.Select(string.Format("nodeID='{0}'",StartFromNodeID))[0]).nodeDescription;
                rootNote = ((TreeData.TreeDataTableRow) dtTree.Select(string.Format("nodeID='{0}'",StartFromNodeID))[0]).nodeNote;
                rootCategory = ((TreeData.TreeDataTableRow)dtTree.Select(string.Format("nodeID='{0}'", StartFromNodeID))[0]).nodeCategory;
                rootSOD = ((TreeData.TreeDataTableRow)dtTree.Select(string.Format("nodeID='{0}'", StartFromNodeID))[0]).nodeSOD;
            }
           
            XmlNode RootNode = GetXMLNode(StartFromNodeID, rootDescription, rootNote,rootCategory,rootSOD);
            nodeTree.AppendChild(RootNode);
            BuildTree(RootNode, 0);

            //check for intersection. line below should be remarked if not debugging
            //as it affects performance measurably.
            //OverlapExists();
            Bitmap bmp = new Bitmap(imgWidth, imgHeight);
            gr = Graphics.FromImage(bmp);
            gr.Clear(_BGColor);
            DrawChart(RootNode);

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
            //after resize, determine the change percentage
            PercentageChangeX =Convert.ToDouble( Width) / imgWidth;
            PercentageChangeY =Convert.ToDouble(  Height) / imgHeight;
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

            Rectangle Result = new Rectangle(X, Y,(int) (_BoxWidth * PercentageChangeX) ,(int)( _BoxHeight * PercentageChangeY) );
            return Result;

        }
        #endregion
        #region Private Methods
        /// <summary>
        /// convert the datatable to an XML document
        /// </summary>
        /// <param name="oNode"></param>
        /// <param name="y"></param>
        private void BuildTree(XmlNode oNode, int y)
        {
            XmlNode childNode = null;
            //has children
            foreach (TreeData.TreeDataTableRow  childRow in dtTree.Select(
                string.Format("parentNodeID='{0}'", oNode.Attributes["nodeID"].InnerText)))
            {
                //for each child node call this function again
                childNode = GetXMLNode(childRow.nodeID, childRow.nodeDescription, childRow.nodeNote,childRow.nodeCategory,childRow.nodeSOD);
                oNode.AppendChild(childNode);
                BuildTree(childNode, y + 1);

            }
            //build node data
            //after checking for nodes we can add the current node
            int StartX;
            int StartY;
            int[] ResultsArr = new int[] {GetXPosByOwnChildren(oNode),
                                    GetXPosByParentPreviousSibling(oNode),
                                    GetXPosByPreviousSibling(oNode),
                                    _Margin };
            Array.Sort(ResultsArr);
            StartX = ResultsArr[3];
            StartY = (y * (_BoxHeight + _VerticalSpace)) + _Margin;
            int width = _BoxWidth;
            int height = _BoxHeight;
            //update the coordinates of this box into the matrix, for later calculations
            oNode.Attributes["X"].InnerText = StartX.ToString();
            oNode.Attributes["Y"].InnerText = StartY.ToString();
            
            //update the image size
            if (imgWidth < (StartX + width + _Margin))
            {
                imgWidth = StartX + width + _Margin;
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
                    
                    Result = Convert.ToInt32(PrevSibling.Attributes["X"].InnerText ) + _BoxWidth + _HorizontalSpace;
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
                   Result= X + _BoxWidth + _HorizontalSpace;
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
        private XmlNode GetXMLNode(string nodeID,string nodeDescription,string nodeNote, string nodeCategory, string nodeSOD)
        {
            //build the node
            XmlNode resultNode = nodeTree.CreateElement("Node");
            XmlAttribute attNodeID = nodeTree.CreateAttribute("nodeID");
            
            XmlAttribute attNodeDescription = nodeTree.CreateAttribute("nodeDescription");
            XmlAttribute attNodeNote = nodeTree.CreateAttribute("nodeNote");
            XmlAttribute attNodeCategory = nodeTree.CreateAttribute("nodeCategory");
            XmlAttribute attNodeSOD = nodeTree.CreateAttribute("nodeSOD");
            XmlAttribute attStartX = nodeTree.CreateAttribute("X");
            XmlAttribute attStartY = nodeTree.CreateAttribute("Y");
            
            //set the values of what we know
            attNodeID.InnerText = nodeID;
            
            attNodeDescription.InnerText=nodeDescription;
            attNodeNote.InnerText=nodeNote;
            attNodeCategory.InnerText = nodeCategory;
            attNodeSOD.InnerText = nodeSOD;
            attStartX.InnerText = "0";
            attStartY.InnerText = "0";
            
            resultNode.Attributes.Append(attNodeID);
            
            resultNode.Attributes.Append(attNodeDescription);
            resultNode.Attributes.Append(attNodeNote);
            resultNode.Attributes.Append(attNodeCategory);
            resultNode.Attributes.Append(attNodeSOD);
            resultNode.Attributes.Append(attStartX);
            resultNode.Attributes.Append(attStartY);
            
            return resultNode;
        
        }

        private Color GetNextColor(System.Collections.Hashtable hashy)
        {
            return colorsToUse[hashy.Count];
        }

        System.Collections.Hashtable peopleHash = new System.Collections.Hashtable(); // Will contain consistent colors by person/category
        System.Collections.Hashtable catHash = new System.Collections.Hashtable(); // Will contain consistent colors by person/category
        /// <summary>
        /// Draws the actual chart image.
        /// </summary>
        private void DrawChart(XmlNode oNode)
        {
            // Create font and brush.
            Font drawFont = new Font(_FontName, _FontSize);
            SolidBrush drawBrush = new SolidBrush(_FontColor);
            SolidBrush drawBrushError = new SolidBrush(Color.Red);

            Pen boxPen = new Pen(_LineColor, _LineWidth);
            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;
            //find children
            
            /**/

            Rectangle currentRectangle = getRectangleFromNode(oNode);

            String drawString = oNode.Attributes["nodeDescription"].InnerText +
                Environment.NewLine +
                oNode.Attributes["nodeNote"].InnerText;

            String drawStringCategory = oNode.Attributes["nodeCategory"].InnerText;
            String drawStringSOD = oNode.Attributes["nodeSOD"].InnerText;

            // Adjust height of the boxes
            Rectangle oldRectangle = currentRectangle;
            //if (drawString.IndexOf("\n") > 100)
            if (drawString.IndexOf("justperson") > -1)
            {
                drawString = drawString.Replace("justperson", "");
                // draw smaller boxes
                int count = drawString.Split('\n').Length - 1;
                int Diff = (count - 2) * 12;
                Size newSize = new Size(currentRectangle.Size.Width, currentRectangle.Size.Height + Diff);
                
                 currentRectangle = new Rectangle(currentRectangle.Location, newSize);
              //  gr.DrawRectangle(boxPen, new_currentRectangle);
              //  gr.FillRectangle(new SolidBrush(_BoxFillColor), new_currentRectangle);
            }
            // else
            {
                // Draw existing box
                gr.DrawRectangle(boxPen, currentRectangle);
                gr.FillRectangle(new SolidBrush(_BoxFillColor), currentRectangle);

       
            }



            // Create string to draw.
            // because we escape apostophes out
            drawString = drawString.Replace("#", "'");

            // Draw string to screen.
            //if (drawString.IndexOf("No person") > -1)
            //{
           //     gr.DrawString(drawString, drawFont, drawBrushError, currentRectangle, drawFormat);
           // }
           // else

            // using this for more than ORG so got rid of the exception 9/27/2017
                gr.DrawString(drawString, drawFont, drawBrush, currentRectangle, drawFormat);



            // draw secondary box 1 - Category - if necessary
            SolidBrush drawBrush_Category = new SolidBrush(Color.Black);
            SolidBrush fillBrush_Category = new SolidBrush(Color.White);
            // draw secondary box 2 - Person - if necessary
            Pen boxPen_CATEGORY = new Pen(Color.Blue, 2);
            Font drawFont_CATEGORY = new Font("trebuchet", 8);

            if (_showCategory)
            {

                Color toUse = Color.Black;
               
                toUse = GetColorByIDX(drawStringCategory, catHash);
                boxPen_CATEGORY = new Pen(toUse, 4);
                currentRectangle = DrawSecondaryBox(new SolidBrush(toUse), fillBrush_Category, boxPen_CATEGORY, drawFont_CATEGORY, drawFormat, currentRectangle.Location.X,
                    currentRectangle.Y, currentRectangle, drawStringCategory, new Size(70, 20));
            }

         


            currentRectangle = oldRectangle;
            //draw connecting lines

            
            if (oNode.ParentNode.Name != "#document")
            {
                //all but the top box should have lines growing out of their top
                gr.DrawLine(boxPen, currentRectangle.Left + (_BoxWidth / 2),
                                            currentRectangle.Top,
                                            currentRectangle.Left + (_BoxWidth / 2),
                                            currentRectangle.Top - (_VerticalSpace / 2));
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
                gr.DrawLine(boxPen,getRectangleFromNode( oNode.PreviousSibling).Left + (_BoxWidth / 2) - (_LineWidth / 2),
                                    getRectangleFromNode(oNode.PreviousSibling).Top - (_VerticalSpace / 2),
                                    currentRectangle.Left + (_BoxWidth / 2) + (_LineWidth / 2),
                                    currentRectangle.Top - (_VerticalSpace / 2));


            }

            ///
            // -- Only shows if necessary
            ///
            if (_showOverridePerson)
            {
                drawBrush_Category = new SolidBrush(Color.White);
                fillBrush_Category = new SolidBrush(Color.Green);
                // draw secondary box 2 - Person - if necessary
                boxPen_CATEGORY = new Pen(Color.Pink, 2);
                drawFont_CATEGORY = new Font("trebuchet", 8);
                string[] people_involved = drawStringSOD.Split(new char[1] { '*' });
                string SOD = people_involved[0].ToString();
                string position_owner = String.Empty;

                if (people_involved.Length > 1)
                    position_owner = people_involved[1].ToString();

                string personLookingAt = "";
                if (SOD != String.Empty) personLookingAt = SOD; else personLookingAt = position_owner;

                

             //   if (personLookingAt != String.Empty)
                {
                    Color toUse = Color.Black;

                    toUse = GetColorByIDX(personLookingAt, peopleHash);

                  
                    string positionLabel = position_owner;

                    //
                    // - Draw a Manager box above at a set location to indicate who is in or mostly in thi sposition currently
                    //
                    if (position_owner == String.Empty)
                    {
                        positionLabel = ">1"; 
                    }

                    currentRectangle = DrawSecondaryBox(drawBrush_Category, new SolidBrush(toUse), boxPen_CATEGORY, drawFont_CATEGORY, drawFormat,
                        currentRectangle.Location.X - (currentRectangle.Width / 2), 15, currentRectangle, positionLabel.ToUpper(), new Size(70, 40));


                    currentRectangle = DrawSecondaryBox(drawBrush_Category, new SolidBrush(toUse), boxPen_CATEGORY, drawFont_CATEGORY, drawFormat,
                        currentRectangle.Location.X, currentRectangle.Y - (currentRectangle.Height / 2), currentRectangle, SOD, new Size(70, 20));

                }
            }
            foreach (XmlNode childNode in oNode.ChildNodes)
            {
                DrawChart(childNode);
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
        private Rectangle DrawSecondaryBox(SolidBrush drawBrush_Category, SolidBrush fillBrush_Category,Pen boxPen_CATEGORY, Font drawFont_CATEGORY,
            StringFormat drawFormat, int Horiz, int Vertical, Rectangle currentRectangle, string name, Size newSize)
        {
            if (name == String.Empty) return currentRectangle;

            Rectangle miniRect = new Rectangle(new Point(Horiz + currentRectangle.Width - 45, Vertical + currentRectangle.Height - 12), newSize);
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
            
            int X=0;
            int newX=0;
            int Y=0;
            int newY=0;
            foreach(XmlNode oNode in nodeTree.SelectNodes("//Node"))
            {
                //go through all nodes and resize the coordinates
                X=Convert.ToInt32(oNode.Attributes["X"].InnerText);
                Y=Convert.ToInt32(oNode.Attributes["Y"].InnerText);
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
            foreach (XmlNode oNode in nodeTree.SelectNodes("//Node"))
            {
                //go through all nodes and resize the coordinates
                X = Convert.ToInt32(oNode.Attributes["X"].InnerText);
                Y = Convert.ToInt32(oNode.Attributes["Y"].InnerText);
                currentRect = new Rectangle(X,Y,_BoxWidth,_BoxHeight);
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
