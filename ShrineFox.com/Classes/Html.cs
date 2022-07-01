using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShrineFoxCom
{
    public class Html
    {
        public static string Notice(string color, string contents)
        {
            return $"<div class=\"notice {color}\"><p>{contents}</p></div>";
        }

        // Creates a card using the Spectre css framework
        public static string CardStart(string title = "", string subtitle = "", string image = "")
        {
            return "<div class=\"card\">" +
                        $"<div class=\"card-image\"><img class=\"img-responsive\" src=\"{image}\"></div>" +
                        "<div class=\"card-header\">" +
                            $"<div class=\"card-title h5\">{title}</div>" +
                            $"<div class=\"card-subtitle text-muted\">{subtitle}</div>" +
                        "</div>" +
                        $"<div class=\"card-body\">";
        }

        public static string CardEnd()
        {
            return "</div>" +
                    $"<div class=\"card-footer\">";
        }

        public static string ContainerStart()
        {
            return "<div class=\"container\"><div class=\"columns\">";
        }

        public static string ColumnStart(int widthOneThruTwelve, int wrapWidth = 7)
        {
            return $"<div class=\"column col-{widthOneThruTwelve} col-sm-{wrapWidth}\">";
        }
        public static string ColumnEnd()
        {
            return "</div>";
        }

        public static string ContainerEnd()
        {
            return "</div></div>";
        }
    }
}