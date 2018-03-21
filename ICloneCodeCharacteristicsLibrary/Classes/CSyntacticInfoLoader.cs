using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ICloneCodeCharacteristicsLibrary.Classes
{
   /// <summary>
   /// 
   /// </summary>
   public sealed class CSyntacticInfoLoader
   {
      private string filename;
      List<string> info_strings;

      /// <summary>
      /// 
      /// </summary>
      /// <param name="directory"></param>
      public CSyntacticInfoLoader(string directory)
      {
         filename = Path.Combine(directory, ParserData.INFO_FILENAME);
      }

      void prepare_info()
      {
         info_strings = new List<string>();
         using (FileStream f_stream = new FileStream(filename, FileMode.Open))
         {
            using (StreamReader sreader = new StreamReader(f_stream, ParserData.GetEncoding()))
            {
               string source_str = null;
               while ((source_str = sreader.ReadLine()) != null)
               {
                  info_strings.Add(source_str);
               }
            }
         }
      }

      string get_value(string arg)
      {
         foreach (string val in info_strings)
         {
            if (val.IndexOf(arg) > -1)
               return val.Substring(arg.Length);
         }
         throw new KeyNotFoundException();
      }

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public CSyntacticInfo Load()
      {
         if (!File.Exists(filename))
            throw new FileNotFoundException("File not found", filename);

         CSyntacticInfo info = new CSyntacticInfo();
         prepare_info();
         info.Date = DateTime.Parse(get_value(ParserData.DATE));
         info.MinSyntacticUnitSize = Convert.ToInt64(get_value(ParserData.MINSYNTACTICUNITSIZE));
         info.MaxSyntacticUnitSize = Convert.ToInt64(get_value(ParserData.MAXSYNTACTICUNITSIZE));
         info.TotalSyntacticUnitsCount = Convert.ToInt64(get_value(ParserData.TOTALSYNTACTICUNITSCOUNT));
         info.MediumSyntacticUnitSize = Convert.ToDouble(get_value(ParserData.MEDIUMSYNTACTICUNITSIZE));
         info.Dispersion = Convert.ToDouble(get_value(ParserData.DISPERSION));

         return info;
      }
   }
}