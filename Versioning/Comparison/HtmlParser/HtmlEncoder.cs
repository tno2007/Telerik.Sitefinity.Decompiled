// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Versioning.Comparison.HtmlEncoder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.IO;
using System.Text;

namespace Telerik.Sitefinity.Versioning.Comparison
{
  internal abstract class HtmlEncoder
  {
    public static string Encode(string value)
    {
      StringBuilder stringBuilder = new StringBuilder();
      StringReader stringReader = new StringReader(value);
      int num = stringReader.Read();
      while (true)
      {
        switch (num)
        {
          case -1:
            goto label_257;
          case 34:
            stringBuilder.Append("&quot;");
            break;
          case 38:
            stringBuilder.Append("&amp;");
            break;
          case 60:
            stringBuilder.Append("&lt;");
            break;
          case 62:
            stringBuilder.Append("&gt;");
            break;
          case 128:
            stringBuilder.Append("&euro;");
            break;
          case 160:
            stringBuilder.Append("&nbsp;");
            break;
          case 161:
            stringBuilder.Append("&iexcl;");
            break;
          case 162:
            stringBuilder.Append("&cent;");
            break;
          case 163:
            stringBuilder.Append("&pound;");
            break;
          case 164:
            stringBuilder.Append("&curren;");
            break;
          case 165:
            stringBuilder.Append("&yen;");
            break;
          case 166:
            stringBuilder.Append("&brvbar;");
            break;
          case 167:
            stringBuilder.Append("&sect;");
            break;
          case 168:
            stringBuilder.Append("&uml;");
            break;
          case 169:
            stringBuilder.Append("&copy;");
            break;
          case 170:
            stringBuilder.Append("&ordf;");
            break;
          case 171:
            stringBuilder.Append("&laquo;");
            break;
          case 172:
            stringBuilder.Append("&not;");
            break;
          case 173:
            stringBuilder.Append("&shy;");
            break;
          case 174:
            stringBuilder.Append("&reg;");
            break;
          case 175:
            stringBuilder.Append("&macr;");
            break;
          case 176:
            stringBuilder.Append("&deg;");
            break;
          case 177:
            stringBuilder.Append("&plusmn;");
            break;
          case 178:
            stringBuilder.Append("&sup2;");
            break;
          case 179:
            stringBuilder.Append("&sup3;");
            break;
          case 180:
            stringBuilder.Append("&acute;");
            break;
          case 181:
            stringBuilder.Append("&micro;");
            break;
          case 182:
            stringBuilder.Append("&para;");
            break;
          case 183:
            stringBuilder.Append("&middot;");
            break;
          case 184:
            stringBuilder.Append("&cedil;");
            break;
          case 185:
            stringBuilder.Append("&sup1;");
            break;
          case 186:
            stringBuilder.Append("&ordm;");
            break;
          case 187:
            stringBuilder.Append("&raquo;");
            break;
          case 188:
            stringBuilder.Append("&frac14;");
            break;
          case 189:
            stringBuilder.Append("&frac12;");
            break;
          case 190:
            stringBuilder.Append("&frac34;");
            break;
          case 191:
            stringBuilder.Append("&iquest;");
            break;
          case 192:
            stringBuilder.Append("&Agrave;");
            break;
          case 193:
            stringBuilder.Append("&Aacute;");
            break;
          case 194:
            stringBuilder.Append("&Acirc;");
            break;
          case 195:
            stringBuilder.Append("&Atilde;");
            break;
          case 196:
            stringBuilder.Append("&Auml;");
            break;
          case 197:
            stringBuilder.Append("&Aring;");
            break;
          case 198:
            stringBuilder.Append("&AElig;");
            break;
          case 199:
            stringBuilder.Append("&Ccedil;");
            break;
          case 200:
            stringBuilder.Append("&Egrave;");
            break;
          case 201:
            stringBuilder.Append("&Eacute;");
            break;
          case 202:
            stringBuilder.Append("&Ecirc;");
            break;
          case 203:
            stringBuilder.Append("&Euml;");
            break;
          case 204:
            stringBuilder.Append("&Igrave;");
            break;
          case 205:
            stringBuilder.Append("&Iacute;");
            break;
          case 206:
            stringBuilder.Append("&Icirc;");
            break;
          case 207:
            stringBuilder.Append("&Iuml;");
            break;
          case 208:
            stringBuilder.Append("&ETH;");
            break;
          case 209:
            stringBuilder.Append("&Ntilde;");
            break;
          case 210:
            stringBuilder.Append("&Ograve;");
            break;
          case 211:
            stringBuilder.Append("&Oacute;");
            break;
          case 212:
            stringBuilder.Append("&Ocirc;");
            break;
          case 213:
            stringBuilder.Append("&Otilde;");
            break;
          case 214:
            stringBuilder.Append("&Ouml;");
            break;
          case 215:
            stringBuilder.Append("&times;");
            break;
          case 216:
            stringBuilder.Append("&Oslash;");
            break;
          case 217:
            stringBuilder.Append("&Ugrave;");
            break;
          case 218:
            stringBuilder.Append("&Uacute;");
            break;
          case 219:
            stringBuilder.Append("&Ucirc;");
            break;
          case 220:
            stringBuilder.Append("&Uuml;");
            break;
          case 221:
            stringBuilder.Append("&Yacute;");
            break;
          case 222:
            stringBuilder.Append("&THORN;");
            break;
          case 223:
            stringBuilder.Append("&szlig;");
            break;
          case 224:
            stringBuilder.Append("&agrave;");
            break;
          case 225:
            stringBuilder.Append("&aacute;");
            break;
          case 226:
            stringBuilder.Append("&acirc;");
            break;
          case 227:
            stringBuilder.Append("&atilde;");
            break;
          case 228:
            stringBuilder.Append("&auml;");
            break;
          case 229:
            stringBuilder.Append("&aring;");
            break;
          case 230:
            stringBuilder.Append("&aelig;");
            break;
          case 231:
            stringBuilder.Append("&ccedil;");
            break;
          case 232:
            stringBuilder.Append("&egrave;");
            break;
          case 233:
            stringBuilder.Append("&eacute;");
            break;
          case 234:
            stringBuilder.Append("&ecirc;");
            break;
          case 235:
            stringBuilder.Append("&euml;");
            break;
          case 236:
            stringBuilder.Append("&igrave;");
            break;
          case 237:
            stringBuilder.Append("&iacute;");
            break;
          case 238:
            stringBuilder.Append("&icirc;");
            break;
          case 239:
            stringBuilder.Append("&iuml;");
            break;
          case 240:
            stringBuilder.Append("&eth;");
            break;
          case 241:
            stringBuilder.Append("&ntilde;");
            break;
          case 242:
            stringBuilder.Append("&ograve;");
            break;
          case 243:
            stringBuilder.Append("&oacute;");
            break;
          case 244:
            stringBuilder.Append("&ocirc;");
            break;
          case 245:
            stringBuilder.Append("&otilde;");
            break;
          case 246:
            stringBuilder.Append("&ouml;");
            break;
          case 247:
            stringBuilder.Append("&divide;");
            break;
          case 248:
            stringBuilder.Append("&oslash;");
            break;
          case 249:
            stringBuilder.Append("&ugrave;");
            break;
          case 250:
            stringBuilder.Append("&uacute;");
            break;
          case 251:
            stringBuilder.Append("&ucirc;");
            break;
          case 252:
            stringBuilder.Append("&uuml;");
            break;
          case 253:
            stringBuilder.Append("&yacute;");
            break;
          case 254:
            stringBuilder.Append("&thorn;");
            break;
          case (int) byte.MaxValue:
            stringBuilder.Append("&yuml;");
            break;
          case 338:
            stringBuilder.Append("&OElig;");
            break;
          case 339:
            stringBuilder.Append("&oelig;");
            break;
          case 352:
            stringBuilder.Append("&Scaron;");
            break;
          case 353:
            stringBuilder.Append("&scaron;");
            break;
          case 376:
            stringBuilder.Append("&Yuml;");
            break;
          case 402:
            stringBuilder.Append("&fnof;");
            break;
          case 710:
            stringBuilder.Append("&circ;");
            break;
          case 732:
            stringBuilder.Append("&tilde;");
            break;
          case 913:
            stringBuilder.Append("&Alpha;");
            break;
          case 914:
            stringBuilder.Append("&Beta;");
            break;
          case 915:
            stringBuilder.Append("&Gamma;");
            break;
          case 916:
            stringBuilder.Append("&Delta;");
            break;
          case 917:
            stringBuilder.Append("&Epsilon;");
            break;
          case 918:
            stringBuilder.Append("&Zeta;");
            break;
          case 919:
            stringBuilder.Append("&Eta;");
            break;
          case 920:
            stringBuilder.Append("&Theta;");
            break;
          case 921:
            stringBuilder.Append("&Iota;");
            break;
          case 922:
            stringBuilder.Append("&Kappa;");
            break;
          case 923:
            stringBuilder.Append("&Lambda;");
            break;
          case 924:
            stringBuilder.Append("&Mu;");
            break;
          case 925:
            stringBuilder.Append("&Nu;");
            break;
          case 926:
            stringBuilder.Append("&Xi;");
            break;
          case 927:
            stringBuilder.Append("&Omicron;");
            break;
          case 928:
            stringBuilder.Append("&Pi;");
            break;
          case 929:
            stringBuilder.Append("&Rho;");
            break;
          case 931:
            stringBuilder.Append("&Sigma;");
            break;
          case 932:
            stringBuilder.Append("&Tau;");
            break;
          case 933:
            stringBuilder.Append("&Upsilon;");
            break;
          case 934:
            stringBuilder.Append("&Phi;");
            break;
          case 935:
            stringBuilder.Append("&Chi;");
            break;
          case 936:
            stringBuilder.Append("&Psi;");
            break;
          case 937:
            stringBuilder.Append("&Omega;");
            break;
          case 945:
            stringBuilder.Append("&alpha;");
            break;
          case 946:
            stringBuilder.Append("&beta;");
            break;
          case 947:
            stringBuilder.Append("&gamma;");
            break;
          case 948:
            stringBuilder.Append("&delta;");
            break;
          case 949:
            stringBuilder.Append("&epsilon;");
            break;
          case 950:
            stringBuilder.Append("&zeta;");
            break;
          case 951:
            stringBuilder.Append("&eta;");
            break;
          case 952:
            stringBuilder.Append("&theta;");
            break;
          case 953:
            stringBuilder.Append("&iota;");
            break;
          case 954:
            stringBuilder.Append("&kappa;");
            break;
          case 955:
            stringBuilder.Append("&lambda;");
            break;
          case 956:
            stringBuilder.Append("&mu;");
            break;
          case 957:
            stringBuilder.Append("&nu;");
            break;
          case 958:
            stringBuilder.Append("&xi;");
            break;
          case 959:
            stringBuilder.Append("&omicron;");
            break;
          case 960:
            stringBuilder.Append("&pi;");
            break;
          case 961:
            stringBuilder.Append("&rho;");
            break;
          case 962:
            stringBuilder.Append("&sigmaf;");
            break;
          case 963:
            stringBuilder.Append("&sigma;");
            break;
          case 964:
            stringBuilder.Append("&tau;");
            break;
          case 965:
            stringBuilder.Append("&upsilon;");
            break;
          case 966:
            stringBuilder.Append("&phi;");
            break;
          case 967:
            stringBuilder.Append("&chi;");
            break;
          case 968:
            stringBuilder.Append("&psi;");
            break;
          case 969:
            stringBuilder.Append("&omega;");
            break;
          case 977:
            stringBuilder.Append("&thetasym;");
            break;
          case 978:
            stringBuilder.Append("&upsih;");
            break;
          case 982:
            stringBuilder.Append("&piv;");
            break;
          case 8195:
            stringBuilder.Append("&emsp;");
            break;
          case 8201:
            stringBuilder.Append("&thinsp;");
            break;
          case 8204:
            stringBuilder.Append("&zwnj;");
            break;
          case 8205:
            stringBuilder.Append("&zwj;");
            break;
          case 8206:
            stringBuilder.Append("&lrm;");
            break;
          case 8207:
            stringBuilder.Append("&rlm;");
            break;
          case 8211:
            stringBuilder.Append("&ndash;");
            break;
          case 8212:
            stringBuilder.Append("&mdash;");
            break;
          case 8216:
            stringBuilder.Append("&lsquo;");
            break;
          case 8217:
            stringBuilder.Append("&rsquo;");
            break;
          case 8218:
            stringBuilder.Append("&sbquo;");
            break;
          case 8220:
            stringBuilder.Append("&ldquo;");
            break;
          case 8221:
            stringBuilder.Append("&rdquo;");
            break;
          case 8222:
            stringBuilder.Append("&bdquo;");
            break;
          case 8224:
            stringBuilder.Append("&dagger;");
            break;
          case 8225:
            stringBuilder.Append("&Dagger;");
            break;
          case 8226:
            stringBuilder.Append("&bull;");
            break;
          case 8230:
            stringBuilder.Append("&hellip;");
            break;
          case 8240:
            stringBuilder.Append("&permil;");
            break;
          case 8242:
            stringBuilder.Append("&prime;");
            break;
          case 8243:
            stringBuilder.Append("&Prime;");
            break;
          case 8249:
            stringBuilder.Append("&lsaquo;");
            break;
          case 8250:
            stringBuilder.Append("&rsaquo;");
            break;
          case 8254:
            stringBuilder.Append("&oline;");
            break;
          case 8260:
            stringBuilder.Append("&fras1;");
            break;
          case 8465:
            stringBuilder.Append("&image;");
            break;
          case 8472:
            stringBuilder.Append("&weierp;");
            break;
          case 8476:
            stringBuilder.Append("&real;");
            break;
          case 8482:
            stringBuilder.Append("&trade;");
            break;
          case 8501:
            stringBuilder.Append("&alefsym;");
            break;
          case 8592:
            stringBuilder.Append("&larr;");
            break;
          case 8593:
            stringBuilder.Append("&uarr;");
            break;
          case 8594:
            stringBuilder.Append("&rarr;");
            break;
          case 8595:
            stringBuilder.Append("&darr;");
            break;
          case 8596:
            stringBuilder.Append("&harr;");
            break;
          case 8629:
            stringBuilder.Append("&crarr;");
            break;
          case 8656:
            stringBuilder.Append("&lArr;");
            break;
          case 8657:
            stringBuilder.Append("&uArr;");
            break;
          case 8658:
            stringBuilder.Append("&rArr;");
            break;
          case 8659:
            stringBuilder.Append("&dArr;");
            break;
          case 8660:
            stringBuilder.Append("&hArr;");
            break;
          case 8704:
            stringBuilder.Append("&forall;");
            break;
          case 8706:
            stringBuilder.Append("&part;");
            break;
          case 8707:
            stringBuilder.Append("&exist;");
            break;
          case 8709:
            stringBuilder.Append("&empty;");
            break;
          case 8711:
            stringBuilder.Append("&nabla;");
            break;
          case 8712:
            stringBuilder.Append("&isin;");
            break;
          case 8713:
            stringBuilder.Append("&notin;");
            break;
          case 8715:
            stringBuilder.Append("&ni;");
            break;
          case 8719:
            stringBuilder.Append("&prod;");
            break;
          case 8721:
            stringBuilder.Append("&sum;");
            break;
          case 8722:
            stringBuilder.Append("&minus;");
            break;
          case 8727:
            stringBuilder.Append("&lowast;");
            break;
          case 8730:
            stringBuilder.Append("&radic;");
            break;
          case 8733:
            stringBuilder.Append("&prop;");
            break;
          case 8734:
            stringBuilder.Append("&infin;");
            break;
          case 8736:
            stringBuilder.Append("&ang;");
            break;
          case 8743:
            stringBuilder.Append("&and;");
            break;
          case 8744:
            stringBuilder.Append("&or;");
            break;
          case 8745:
            stringBuilder.Append("&cap;");
            break;
          case 8746:
            stringBuilder.Append("&cup;");
            break;
          case 8747:
            stringBuilder.Append("&int;");
            break;
          case 8756:
            stringBuilder.Append("&there4;");
            break;
          case 8764:
            stringBuilder.Append("&sim;");
            break;
          case 8773:
            stringBuilder.Append("&cong;");
            break;
          case 8776:
            stringBuilder.Append("&asymp;");
            break;
          case 8800:
            stringBuilder.Append("&ne;");
            break;
          case 8801:
            stringBuilder.Append("&equiv;");
            break;
          case 8804:
            stringBuilder.Append("&le;");
            break;
          case 8805:
            stringBuilder.Append("&ge;");
            break;
          case 8834:
            stringBuilder.Append("&sub;");
            break;
          case 8835:
            stringBuilder.Append("&sup;");
            break;
          case 8836:
            stringBuilder.Append("&nsub;");
            break;
          case 8838:
            stringBuilder.Append("&sube;");
            break;
          case 8839:
            stringBuilder.Append("&supe;");
            break;
          case 8853:
            stringBuilder.Append("&oplus;");
            break;
          case 8855:
            stringBuilder.Append("&otimes;");
            break;
          case 8869:
            stringBuilder.Append("&perp;");
            break;
          case 8901:
            stringBuilder.Append("&sdot;");
            break;
          case 8968:
            stringBuilder.Append("&lceil;");
            break;
          case 8969:
            stringBuilder.Append("&rceil;");
            break;
          case 8970:
            stringBuilder.Append("&lfloor;");
            break;
          case 8971:
            stringBuilder.Append("&rfloor;");
            break;
          case 9001:
            stringBuilder.Append("&lang;");
            break;
          case 9002:
            stringBuilder.Append("&rang;");
            break;
          case 9674:
            stringBuilder.Append("&loz;");
            break;
          case 9824:
            stringBuilder.Append("&spades;");
            break;
          case 9827:
            stringBuilder.Append("&clubs;");
            break;
          case 9829:
            stringBuilder.Append("&hearts;");
            break;
          case 9830:
            stringBuilder.Append("&diams;");
            break;
          default:
            if (num <= (int) sbyte.MaxValue)
            {
              stringBuilder.Append((char) num);
              break;
            }
            stringBuilder.Append("&#" + (object) num + ";");
            break;
        }
        num = stringReader.Read();
      }
label_257:
      return stringBuilder.ToString();
    }

    public static string Decode(string value)
    {
      StringBuilder stringBuilder1 = new StringBuilder();
      StringReader stringReader = new StringReader(value);
      int num1 = stringReader.Read();
      while (num1 != -1)
      {
        StringBuilder stringBuilder2 = new StringBuilder();
        for (; num1 != 38 && num1 != -1; num1 = stringReader.Read())
          stringBuilder2.Append((char) num1);
        stringBuilder1.Append(stringBuilder2.ToString());
        if (num1 == 38)
        {
          StringBuilder stringBuilder3 = new StringBuilder();
          for (; num1 != 59 && num1 != -1; num1 = stringReader.Read())
            stringBuilder3.Append((char) num1);
          if (num1 == 59)
          {
            num1 = stringReader.Read();
            stringBuilder3.Append(';');
            if (stringBuilder3[1] == '#')
            {
              int num2 = int.Parse(stringBuilder3.ToString().Substring(2, stringBuilder3.Length - 3));
              stringBuilder1.Append((char) num2);
            }
            else
            {
              switch (stringBuilder3.ToString())
              {
                case "&AElig;":
                  stringBuilder1.Append('Æ');
                  continue;
                case "&Aacute;":
                  stringBuilder1.Append('Á');
                  continue;
                case "&Acirc;":
                  stringBuilder1.Append('Â');
                  continue;
                case "&Agrave;":
                  stringBuilder1.Append('À');
                  continue;
                case "&Alpha;":
                  stringBuilder1.Append('Α');
                  continue;
                case "&Aring;":
                  stringBuilder1.Append('Å');
                  continue;
                case "&Atilde;":
                  stringBuilder1.Append('Ã');
                  continue;
                case "&Auml;":
                  stringBuilder1.Append('Ä');
                  continue;
                case "&Beta;":
                  stringBuilder1.Append('Β');
                  continue;
                case "&Ccedil;":
                  stringBuilder1.Append('Ç');
                  continue;
                case "&Chi;":
                  stringBuilder1.Append('Χ');
                  continue;
                case "&Dagger;":
                  stringBuilder1.Append('‡');
                  continue;
                case "&Delta;":
                  stringBuilder1.Append('Δ');
                  continue;
                case "&ETH;":
                  stringBuilder1.Append('Ð');
                  continue;
                case "&Eacute;":
                  stringBuilder1.Append('É');
                  continue;
                case "&Ecirc;":
                  stringBuilder1.Append('Ê');
                  continue;
                case "&Egrave;":
                  stringBuilder1.Append('È');
                  continue;
                case "&Epsilon;":
                  stringBuilder1.Append('Ε');
                  continue;
                case "&Eta;":
                  stringBuilder1.Append('Η');
                  continue;
                case "&Euml;":
                  stringBuilder1.Append('Ë');
                  continue;
                case "&Gamma;":
                  stringBuilder1.Append('Γ');
                  continue;
                case "&Iacute;":
                  stringBuilder1.Append('Í');
                  continue;
                case "&Icirc;":
                  stringBuilder1.Append('Î');
                  continue;
                case "&Igrave;":
                  stringBuilder1.Append('Ì');
                  continue;
                case "&Iota;":
                  stringBuilder1.Append('Ι');
                  continue;
                case "&Iuml;":
                  stringBuilder1.Append('Ï');
                  continue;
                case "&Kappa;":
                  stringBuilder1.Append('Κ');
                  continue;
                case "&Lambda;":
                  stringBuilder1.Append('Λ');
                  continue;
                case "&Mu;":
                  stringBuilder1.Append('Μ');
                  continue;
                case "&Ntilde;":
                  stringBuilder1.Append('Ñ');
                  continue;
                case "&Nu;":
                  stringBuilder1.Append('Ν');
                  continue;
                case "&OElig;":
                  stringBuilder1.Append('Œ');
                  continue;
                case "&Oacute;":
                  stringBuilder1.Append('Ó');
                  continue;
                case "&Ocirc;":
                  stringBuilder1.Append('Ô');
                  continue;
                case "&Ograve;":
                  stringBuilder1.Append('Ò');
                  continue;
                case "&Omega;":
                  stringBuilder1.Append('Ω');
                  continue;
                case "&Omicron;":
                  stringBuilder1.Append('Ο');
                  continue;
                case "&Oslash;":
                  stringBuilder1.Append('Ø');
                  continue;
                case "&Otilde;":
                  stringBuilder1.Append('Õ');
                  continue;
                case "&Ouml;":
                  stringBuilder1.Append('Ö');
                  continue;
                case "&Phi;":
                  stringBuilder1.Append('Φ');
                  continue;
                case "&Pi;":
                  stringBuilder1.Append('Π');
                  continue;
                case "&Prime;":
                  stringBuilder1.Append('″');
                  continue;
                case "&Psi;":
                  stringBuilder1.Append('Ψ');
                  continue;
                case "&Rho;":
                  stringBuilder1.Append('Ρ');
                  continue;
                case "&Scaron;":
                  stringBuilder1.Append('Š');
                  continue;
                case "&Sigma;":
                  stringBuilder1.Append('Σ');
                  continue;
                case "&THORN;":
                  stringBuilder1.Append('Þ');
                  continue;
                case "&Tau;":
                  stringBuilder1.Append('Τ');
                  continue;
                case "&Theta;":
                  stringBuilder1.Append('Θ');
                  continue;
                case "&Uacute;":
                  stringBuilder1.Append('Ú');
                  continue;
                case "&Ucirc;":
                  stringBuilder1.Append('Û');
                  continue;
                case "&Ugrave;":
                  stringBuilder1.Append('Ù');
                  continue;
                case "&Upsilon;":
                  stringBuilder1.Append('Υ');
                  continue;
                case "&Uuml;":
                  stringBuilder1.Append('Ü');
                  continue;
                case "&Xi;":
                  stringBuilder1.Append('Ξ');
                  continue;
                case "&Yacute;":
                  stringBuilder1.Append('Ý');
                  continue;
                case "&Yuml;":
                  stringBuilder1.Append('Ÿ');
                  continue;
                case "&Zeta;":
                  stringBuilder1.Append('Ζ');
                  continue;
                case "&aacute;":
                  stringBuilder1.Append('á');
                  continue;
                case "&acirc;":
                  stringBuilder1.Append('â');
                  continue;
                case "&acute;":
                  stringBuilder1.Append('´');
                  continue;
                case "&aelig;":
                  stringBuilder1.Append('æ');
                  continue;
                case "&agrave;":
                  stringBuilder1.Append('à');
                  continue;
                case "&alefsym;":
                  stringBuilder1.Append('ℵ');
                  continue;
                case "&alpha;":
                  stringBuilder1.Append('α');
                  continue;
                case "&amp;":
                  stringBuilder1.Append("&");
                  continue;
                case "&and;":
                  stringBuilder1.Append('∧');
                  continue;
                case "&ang;":
                  stringBuilder1.Append('∠');
                  continue;
                case "&aring;":
                  stringBuilder1.Append('å');
                  continue;
                case "&asymp;":
                  stringBuilder1.Append('≈');
                  continue;
                case "&atilde;":
                  stringBuilder1.Append('ã');
                  continue;
                case "&auml;":
                  stringBuilder1.Append('ä');
                  continue;
                case "&bdquo;":
                  stringBuilder1.Append('„');
                  continue;
                case "&beta;":
                  stringBuilder1.Append('β');
                  continue;
                case "&brvbar;":
                  stringBuilder1.Append('¦');
                  continue;
                case "&bull;":
                  stringBuilder1.Append('•');
                  continue;
                case "&cap;":
                  stringBuilder1.Append('∩');
                  continue;
                case "&ccedil;":
                  stringBuilder1.Append('ç');
                  continue;
                case "&cedil;":
                  stringBuilder1.Append('¸');
                  continue;
                case "&cent;":
                  stringBuilder1.Append('¢');
                  continue;
                case "&chi;":
                  stringBuilder1.Append('χ');
                  continue;
                case "&circ;":
                  stringBuilder1.Append('ˆ');
                  continue;
                case "&clubs;":
                  stringBuilder1.Append('♣');
                  continue;
                case "&cong;":
                  stringBuilder1.Append('≅');
                  continue;
                case "&copy;":
                  stringBuilder1.Append('©');
                  continue;
                case "&crarr;":
                  stringBuilder1.Append('↵');
                  continue;
                case "&cup;":
                  stringBuilder1.Append('∪');
                  continue;
                case "&curren;":
                  stringBuilder1.Append('¤');
                  continue;
                case "&dArr;":
                  stringBuilder1.Append('⇓');
                  continue;
                case "&dagger;":
                  stringBuilder1.Append('†');
                  continue;
                case "&darr;":
                  stringBuilder1.Append('↓');
                  continue;
                case "&deg;":
                  stringBuilder1.Append('°');
                  continue;
                case "&delta;":
                  stringBuilder1.Append('δ');
                  continue;
                case "&diams;":
                  stringBuilder1.Append('♦');
                  continue;
                case "&divide;":
                  stringBuilder1.Append('÷');
                  continue;
                case "&eacute;":
                  stringBuilder1.Append('é');
                  continue;
                case "&ecirc;":
                  stringBuilder1.Append('ê');
                  continue;
                case "&egrave;":
                  stringBuilder1.Append('è');
                  continue;
                case "&empty;":
                  stringBuilder1.Append('∅');
                  continue;
                case "&emsp;":
                  stringBuilder1.Append(' ');
                  continue;
                case "&epsilon;":
                  stringBuilder1.Append('ε');
                  continue;
                case "&equiv;":
                  stringBuilder1.Append('≡');
                  continue;
                case "&eta;":
                  stringBuilder1.Append('η');
                  continue;
                case "&eth;":
                  stringBuilder1.Append('ð');
                  continue;
                case "&euml;":
                  stringBuilder1.Append('ë');
                  continue;
                case "&euro;":
                  stringBuilder1.Append('\u0080');
                  continue;
                case "&exist;":
                  stringBuilder1.Append('∃');
                  continue;
                case "&fnof;":
                  stringBuilder1.Append('ƒ');
                  continue;
                case "&forall;":
                  stringBuilder1.Append('∀');
                  continue;
                case "&frac12;":
                  stringBuilder1.Append('\u00BD');
                  continue;
                case "&frac14;":
                  stringBuilder1.Append('\u00BC');
                  continue;
                case "&frac34;":
                  stringBuilder1.Append('\u00BE');
                  continue;
                case "&fras1;":
                  stringBuilder1.Append('⁄');
                  continue;
                case "&gamma;":
                  stringBuilder1.Append('γ');
                  continue;
                case "&ge;":
                  stringBuilder1.Append('≥');
                  continue;
                case "&gt;":
                  stringBuilder1.Append(">");
                  continue;
                case "&hArr;":
                  stringBuilder1.Append('⇔');
                  continue;
                case "&harr;":
                  stringBuilder1.Append('↔');
                  continue;
                case "&hearts;":
                  stringBuilder1.Append('♥');
                  continue;
                case "&hellip;":
                  stringBuilder1.Append('…');
                  continue;
                case "&iacute;":
                  stringBuilder1.Append('í');
                  continue;
                case "&icirc;":
                  stringBuilder1.Append('î');
                  continue;
                case "&iexcl;":
                  stringBuilder1.Append('¡');
                  continue;
                case "&igrave;":
                  stringBuilder1.Append('ì');
                  continue;
                case "&image;":
                  stringBuilder1.Append('ℑ');
                  continue;
                case "&infin;":
                  stringBuilder1.Append('∞');
                  continue;
                case "&int;":
                  stringBuilder1.Append('∫');
                  continue;
                case "&iota;":
                  stringBuilder1.Append('ι');
                  continue;
                case "&iquest;":
                  stringBuilder1.Append('¿');
                  continue;
                case "&isin;":
                  stringBuilder1.Append('∈');
                  continue;
                case "&iuml;":
                  stringBuilder1.Append('ï');
                  continue;
                case "&kappa;":
                  stringBuilder1.Append('κ');
                  continue;
                case "&lArr;":
                  stringBuilder1.Append('⇐');
                  continue;
                case "&lambda;":
                  stringBuilder1.Append('λ');
                  continue;
                case "&lang;":
                  stringBuilder1.Append('〈');
                  continue;
                case "&laquo;":
                  stringBuilder1.Append('«');
                  continue;
                case "&larr;":
                  stringBuilder1.Append('←');
                  continue;
                case "&lceil;":
                  stringBuilder1.Append('⌈');
                  continue;
                case "&ldquo;":
                  stringBuilder1.Append('“');
                  continue;
                case "&le;":
                  stringBuilder1.Append('≤');
                  continue;
                case "&lfloor;":
                  stringBuilder1.Append('⌊');
                  continue;
                case "&lowast;":
                  stringBuilder1.Append('∗');
                  continue;
                case "&loz;":
                  stringBuilder1.Append('◊');
                  continue;
                case "&lrm;":
                  stringBuilder1.Append('\u200E');
                  continue;
                case "&lsaquo;":
                  stringBuilder1.Append('‹');
                  continue;
                case "&lsquo;":
                  stringBuilder1.Append('‘');
                  continue;
                case "&lt;":
                  stringBuilder1.Append("<");
                  continue;
                case "&macr;":
                  stringBuilder1.Append('¯');
                  continue;
                case "&mdash;":
                  stringBuilder1.Append('—');
                  continue;
                case "&micro;":
                  stringBuilder1.Append('µ');
                  continue;
                case "&middot;":
                  stringBuilder1.Append('·');
                  continue;
                case "&minus;":
                  stringBuilder1.Append('−');
                  continue;
                case "&mu;":
                  stringBuilder1.Append('μ');
                  continue;
                case "&nabla;":
                  stringBuilder1.Append('∇');
                  continue;
                case "&nbsp;":
                  stringBuilder1.Append(' ');
                  continue;
                case "&ndash;":
                  stringBuilder1.Append('–');
                  continue;
                case "&ne;":
                  stringBuilder1.Append('≠');
                  continue;
                case "&ni;":
                  stringBuilder1.Append('∋');
                  continue;
                case "&not;":
                  stringBuilder1.Append('¬');
                  continue;
                case "&notin;":
                  stringBuilder1.Append('∉');
                  continue;
                case "&nsub;":
                  stringBuilder1.Append('⊄');
                  continue;
                case "&ntilde;":
                  stringBuilder1.Append('ñ');
                  continue;
                case "&nu;":
                  stringBuilder1.Append('ν');
                  continue;
                case "&oacute;":
                  stringBuilder1.Append('ó');
                  continue;
                case "&ocirc;":
                  stringBuilder1.Append('ô');
                  continue;
                case "&oelig;":
                  stringBuilder1.Append('œ');
                  continue;
                case "&ograve;":
                  stringBuilder1.Append('ò');
                  continue;
                case "&oline;":
                  stringBuilder1.Append('‾');
                  continue;
                case "&omega;":
                  stringBuilder1.Append('ω');
                  continue;
                case "&omicron;":
                  stringBuilder1.Append('ο');
                  continue;
                case "&oplus;":
                  stringBuilder1.Append('⊕');
                  continue;
                case "&or;":
                  stringBuilder1.Append('∨');
                  continue;
                case "&ordf;":
                  stringBuilder1.Append('ª');
                  continue;
                case "&ordm;":
                  stringBuilder1.Append('º');
                  continue;
                case "&oslash;":
                  stringBuilder1.Append('ø');
                  continue;
                case "&otilde;":
                  stringBuilder1.Append('õ');
                  continue;
                case "&otimes;":
                  stringBuilder1.Append('⊗');
                  continue;
                case "&ouml;":
                  stringBuilder1.Append('ö');
                  continue;
                case "&para;":
                  stringBuilder1.Append('¶');
                  continue;
                case "&part;":
                  stringBuilder1.Append('∂');
                  continue;
                case "&permil;":
                  stringBuilder1.Append('‰');
                  continue;
                case "&perp;":
                  stringBuilder1.Append('⊥');
                  continue;
                case "&phi;":
                  stringBuilder1.Append('φ');
                  continue;
                case "&pi;":
                  stringBuilder1.Append('π');
                  continue;
                case "&piv;":
                  stringBuilder1.Append('ϖ');
                  continue;
                case "&plusmn;":
                  stringBuilder1.Append('±');
                  continue;
                case "&pound;":
                  stringBuilder1.Append('£');
                  continue;
                case "&prime;":
                  stringBuilder1.Append('′');
                  continue;
                case "&prod;":
                  stringBuilder1.Append('∏');
                  continue;
                case "&prop;":
                  stringBuilder1.Append('∝');
                  continue;
                case "&psi;":
                  stringBuilder1.Append('ψ');
                  continue;
                case "&quot;":
                  stringBuilder1.Append("\"");
                  continue;
                case "&rArr;":
                  stringBuilder1.Append('⇒');
                  continue;
                case "&radic;":
                  stringBuilder1.Append('√');
                  continue;
                case "&rang;":
                  stringBuilder1.Append('〉');
                  continue;
                case "&raquo;":
                  stringBuilder1.Append('»');
                  continue;
                case "&rarr;":
                  stringBuilder1.Append('→');
                  continue;
                case "&rceil;":
                  stringBuilder1.Append('⌉');
                  continue;
                case "&rdquo;":
                  stringBuilder1.Append('”');
                  continue;
                case "&real;":
                  stringBuilder1.Append('ℜ');
                  continue;
                case "&reg;":
                  stringBuilder1.Append('®');
                  continue;
                case "&rfloor;":
                  stringBuilder1.Append('⌋');
                  continue;
                case "&rho;":
                  stringBuilder1.Append('ρ');
                  continue;
                case "&rlm;":
                  stringBuilder1.Append('\u200F');
                  continue;
                case "&rsaquo;":
                  stringBuilder1.Append('›');
                  continue;
                case "&rsquo;":
                  stringBuilder1.Append('’');
                  continue;
                case "&sbquo;":
                  stringBuilder1.Append('‚');
                  continue;
                case "&scaron;":
                  stringBuilder1.Append('š');
                  continue;
                case "&sdot;":
                  stringBuilder1.Append('⋅');
                  continue;
                case "&sect;":
                  stringBuilder1.Append('§');
                  continue;
                case "&shy;":
                  stringBuilder1.Append('\u00AD');
                  continue;
                case "&sigma;":
                  stringBuilder1.Append('σ');
                  continue;
                case "&sigmaf;":
                  stringBuilder1.Append('ς');
                  continue;
                case "&sim;":
                  stringBuilder1.Append('∼');
                  continue;
                case "&spades;":
                  stringBuilder1.Append('♠');
                  continue;
                case "&sub;":
                  stringBuilder1.Append('⊂');
                  continue;
                case "&sube;":
                  stringBuilder1.Append('⊆');
                  continue;
                case "&sum;":
                  stringBuilder1.Append('∑');
                  continue;
                case "&sup1;":
                  stringBuilder1.Append('\u00B9');
                  continue;
                case "&sup2;":
                  stringBuilder1.Append('\u00B2');
                  continue;
                case "&sup3;":
                  stringBuilder1.Append('\u00B3');
                  continue;
                case "&sup;":
                  stringBuilder1.Append('⊃');
                  continue;
                case "&supe;":
                  stringBuilder1.Append('⊇');
                  continue;
                case "&szlig;":
                  stringBuilder1.Append('ß');
                  continue;
                case "&tau;":
                  stringBuilder1.Append('τ');
                  continue;
                case "&there4;":
                  stringBuilder1.Append('∴');
                  continue;
                case "&theta;":
                  stringBuilder1.Append('θ');
                  continue;
                case "&thetasym;":
                  stringBuilder1.Append('ϑ');
                  continue;
                case "&thinsp;":
                  stringBuilder1.Append(' ');
                  continue;
                case "&thorn;":
                  stringBuilder1.Append('þ');
                  continue;
                case "&tilde;":
                  stringBuilder1.Append('˜');
                  continue;
                case "&times;":
                  stringBuilder1.Append('×');
                  continue;
                case "&trade;":
                  stringBuilder1.Append('™');
                  continue;
                case "&uArr;":
                  stringBuilder1.Append('⇑');
                  continue;
                case "&uacute;":
                  stringBuilder1.Append('ú');
                  continue;
                case "&uarr;":
                  stringBuilder1.Append('↑');
                  continue;
                case "&ucirc;":
                  stringBuilder1.Append('û');
                  continue;
                case "&ugrave;":
                  stringBuilder1.Append('ù');
                  continue;
                case "&uml;":
                  stringBuilder1.Append('¨');
                  continue;
                case "&upsih;":
                  stringBuilder1.Append('ϒ');
                  continue;
                case "&upsilon;":
                  stringBuilder1.Append('υ');
                  continue;
                case "&uuml;":
                  stringBuilder1.Append('ü');
                  continue;
                case "&weierp;":
                  stringBuilder1.Append('℘');
                  continue;
                case "&xi;":
                  stringBuilder1.Append('ξ');
                  continue;
                case "&yacute;":
                  stringBuilder1.Append('ý');
                  continue;
                case "&yen;":
                  stringBuilder1.Append('¥');
                  continue;
                case "&yuml;":
                  stringBuilder1.Append('ÿ');
                  continue;
                case "&zeta;":
                  stringBuilder1.Append('ζ');
                  continue;
                case "&zwj;":
                  stringBuilder1.Append('\u200D');
                  continue;
                case "&zwnj;":
                  stringBuilder1.Append('\u200C');
                  continue;
                default:
                  stringBuilder1.Append(stringBuilder3.ToString());
                  continue;
              }
            }
          }
          else
            stringBuilder1.Append(stringBuilder3.ToString());
        }
      }
      return stringBuilder1.ToString();
    }
  }
}
