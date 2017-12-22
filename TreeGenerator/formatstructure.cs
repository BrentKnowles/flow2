using System.Drawing;
using System.Collections.Generic;
namespace TreeGenerator
{
    public enum BoxType
    {
        rect = 0,
        ellipse = 1,
        diamond = 2
    }
    public struct FontDetail
    {
        public string font;
        public int fontsize;
        public Color fontcolor;
        public FontDetail(int i)
        {
            fontsize = 20;
            font = "Courier New";
            fontcolor = Color.Azure;
        }
    }
    public struct BoxDetail
    {
        public Color boxfillcolor;
        public int boxthickness;
        public int gradient; // 0 no, 1 yes
        public Color gradientColor;
        public string name;
        public FontDetail primaryFont;
        public FontDetail secondaryFont;
        public BoxType boxType;
        public BoxDetail(string _name)
        {
            boxfillcolor = Color.Purple;
            boxthickness = 6;
            gradient = 0;
            gradientColor = Color.Purple;
            name = _name;
            primaryFont = new FontDetail(1);
            secondaryFont = new FontDetail(1);
            boxType = BoxType.rect;

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
        //public BoxDetail actionbox;
        // public BoxDetail outcomebox;
        // public BoxDetail defaultbox;
        public List<BoxDetail> boxes;
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
            // actionbox = new BoxDetail("actionbox");
            // outcomebox = new BoxDetail("outcomebox");
            // defaultbox = new BoxDetail("defaultbox");
            secondaryFontSize = 11;
            boxlinewidth = 2;
            calloutboxcolor = Color.Green;
            xmarginextra = 0;
            boxes = new List<BoxDetail>(); // trying for an array of styles to make this more versatile
        }
        public int GetIsGradient(string nodetype)
        {
            int defaultc = 0;
            foreach (BoxDetail box in boxes)
            {
                if (box.name == nodetype)
                {
                    return box.gradient;
                }
                else
                if (box.name == "defaultbox")
                {
                    defaultc = box.gradient;
                }
            }


            return defaultc;

        }

        public BoxType GetBoxType(string nodetype)
        {
            BoxType defaultc = BoxType.rect;
            foreach (BoxDetail box in boxes)
            {
                if (box.name == nodetype)
                {
                    return box.boxType;
                }
                else
                if (box.name == "defaultbox")
                {
                    defaultc = box.boxType;
                }

            }
            return defaultc;
        }
        public Color GetGradientColor(string nodetype)
        {
            Color defaultc = Color.Orange;
            foreach (BoxDetail box in boxes)
            {
                if (box.name == nodetype)
                {
                    return box.gradientColor;
                }
                else
                if (box.name == "defaultbox")
                {
                    defaultc = box.gradientColor;
                }
            }


            return defaultc;


        }

        public Color GetColor(string nodetype)
        {
            Color defaultc = Color.Pink;
            foreach (BoxDetail box in boxes)
            {
                if (box.name == nodetype)
                {
                    return box.boxfillcolor;
                }
                else
                if (box.name == "defaultbox")
                {
                    defaultc = box.boxfillcolor;
                }
            }


            return defaultc;
        }
        public Color GetBoxPrimaryFontColor(string nodetype)
        {
            return GetBoxPrimaryFont(nodetype).fontcolor;
        }
        public int GetBoxPrimaryBoxSize(string nodetype)
        {
            return GetBoxPrimaryFont(nodetype).fontsize;
        }
        public string GetBoxPrimaryFontName(string nodetype)
        {
            return GetBoxPrimaryFont(nodetype).font;
        }
        public Color GetBoxSecondaryFontColor(string nodetype)
        {
            return GetBoxSecondaryFont(nodetype).fontcolor;
        }
        public int GetBoxSecondaryBoxSize(string nodetype)
        {
            return GetBoxSecondaryFont(nodetype).fontsize;
        }
        public string GetBoxSecondaryFontName(string nodetype)
        {
            return GetBoxSecondaryFont(nodetype).font;
        }
        private FontDetail GetBoxPrimaryFont(string nodetype)
        {
            FontDetail defaultc = new FontDetail(1);
            foreach (BoxDetail box in boxes)
            {
                if (box.name == nodetype)
                {
                    return box.primaryFont;
                }
                else
                if (box.name == "defaultbox")
                {
                    defaultc = box.primaryFont; ;
                }
            }

            return defaultc;
        }
        public FontDetail GetBoxSecondaryFont(string nodetype)
        {
            FontDetail defaultc = new FontDetail(1);
            foreach (BoxDetail box in boxes)
            {
                if (box.name == nodetype)
                {
                    return box.secondaryFont;
                }
                else
                if (box.name == "defaultbox")
                {
                    defaultc = box.secondaryFont; ;
                }
            }

            return defaultc;
        }
        public int GetBoxSize(string nodetype)
        {

            int defaultc = 9;
            foreach (BoxDetail box in boxes)
            {
                if (box.name == nodetype)
                {
                    return box.boxthickness;
                }
                else
                if (box.name == "defaultbox")
                {
                    defaultc = box.boxthickness;
                }
            }


            return defaultc;


        }
        /*   public int GetBoxFontSize(string nodetype)
           {

               int defaultc = 9;
               foreach (BoxDetail box in boxes)
               {
                   if (box.name == nodetype)
                   {
                       return box.fontsize;
                   }
                   else
                   if (box.name == "defaultbox")
                   {
                       defaultc = box.fontsize;
                   }
               }


               return defaultc;


           }*/
      /*  public string GetBoxFont(string nodetype)
        {

            string defaultc = "Times New Roman";
            foreach (BoxDetail box in boxes)
            {
                if (box.name == nodetype)
                {
                    return box.font;
                }
                else
                if (box.name == "defaultbox")
                {
                    defaultc = box.font;
                }
            }


            return defaultc;


        }*/
    }
}
