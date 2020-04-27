using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GrafolitNOZ.Helpers
{
    public class RandomColorHelper
    {
        private Random rnd;
        private List<ColorHelper> colors;

        public RandomColorHelper()
        {
            rnd = new Random();
            colors = ColorHelper.PopulateColorList();
        }

        private class ColorHelper
        {
            public string ColorName { get; set; }
            public bool IsSelected { get; set; } = false;//auto property

            public static List<ColorHelper> PopulateColorList()
            {
                return new List<ColorHelper>
                {
                    new ColorHelper{ ColorName="#F0F8FF" },
                    new ColorHelper{ ColorName="#FAEBD7" },
                    new ColorHelper{ ColorName="#00FFFF" },
                    new ColorHelper{ ColorName="#7FFFD4" },
                    new ColorHelper{ ColorName="#F0FFFF" },
                    new ColorHelper{ ColorName="#F5F5DC" },
                    new ColorHelper{ ColorName="#FFE4C4" },
                    new ColorHelper{ ColorName="#FFEBCD" },
                    new ColorHelper{ ColorName="#DEB887" },
                    new ColorHelper{ ColorName="#6495ED" },
                    new ColorHelper{ ColorName="#FFF8DC" },
                    new ColorHelper{ ColorName="#DC143C" },
                    new ColorHelper{ ColorName="#00FFFF" },
                    new ColorHelper{ ColorName="#FFF0F5" },
                    new ColorHelper{ ColorName="#E6E6FA" },
                    new ColorHelper{ ColorName="#FFFACD" },
                    new ColorHelper{ ColorName="#E0FFFF" },
                    new ColorHelper{ ColorName="#FAFAD2" },
                    new ColorHelper{ ColorName="#D3D3D3" },
                    new ColorHelper{ ColorName="#FFA07A" },
                    new ColorHelper{ ColorName="#FFFFE0" }
                };
            }
        }

        public string GetNextUnselectedColor()
        {
            var unselectedColors = colors.Where(c => !c.IsSelected).ToList();

            int next = rnd.Next(0, unselectedColors.Count - 1);

            var color = unselectedColors[next];

            colors.Where(c => c.ColorName == color.ColorName).FirstOrDefault().IsSelected = true;

            return color.ColorName;
        }
    }
    
}