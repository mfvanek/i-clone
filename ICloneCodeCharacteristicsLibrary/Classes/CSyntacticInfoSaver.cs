using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ICloneCodeCharacteristicsLibrary.Classes
{
   internal struct ParserData
   {
      public static string INFO_FILENAME = "syntactic_info.icch";
      public static string DATE = "DATE:";
      public static string MINSYNTACTICUNITSIZE = "MINSYNTACTICUNITSIZE:";
      public static string MAXSYNTACTICUNITSIZE = "MAXSYNTACTICUNITSIZE:";
      public static string TOTALSYNTACTICUNITSCOUNT = "TOTALSYNTACTICUNITSCOUNT:";
      public static string MEDIUMSYNTACTICUNITSIZE = "MEDIUMSYNTACTICUNITSIZE:";
      public static string DISPERSION = "DISPERSION:";
      public static string MEANSQUAREDEVIATION = "MEANSQUAREDEVIATION:";
      public static string KMIN = "KMIN:";

      public static Encoding GetEncoding()
      {
         return Encoding.Default;
      }
   }

   /// <summary>
   /// 
   /// </summary>
   public sealed class CSyntacticInfoSaver
   {
      CSyntacticInfo m_info;
      string filename;

      /// <summary>
      /// 
      /// </summary>
      /// <param name="info"></param>
      /// <param name="directory"></param>
      public CSyntacticInfoSaver(CSyntacticInfo info, string directory)
      {
         m_info = info;
         filename = Path.Combine(directory, ParserData.INFO_FILENAME);
      }

      private List<string> prepare_info()
      {
         List<string> info = new List<string>();

         info.Add(ParserData.DATE + m_info.Date.ToString());
         info.Add(ParserData.MINSYNTACTICUNITSIZE + m_info.MinSyntacticUnitSize.ToString());
         info.Add(ParserData.MAXSYNTACTICUNITSIZE + m_info.MaxSyntacticUnitSize.ToString());
         info.Add(ParserData.TOTALSYNTACTICUNITSCOUNT + m_info.TotalSyntacticUnitsCount.ToString());
         info.Add(ParserData.MEDIUMSYNTACTICUNITSIZE + m_info.MediumSyntacticUnitSize.ToString());
         info.Add(ParserData.DISPERSION + m_info.Dispersion.ToString());
         info.Add(ParserData.MEANSQUAREDEVIATION + m_info.MeanSquareDeviation.ToString());
         info.Add(ParserData.KMIN + m_info.Kmin.ToString());

         return info;
      }

      /// <summary>
      /// 
      /// </summary>
      public void Save()
      {
         List<string> info = prepare_info();

         using (FileStream f_stream = new FileStream(filename, FileMode.CreateNew))
         {
            using (StreamWriter s_writer = new StreamWriter(f_stream, ParserData.GetEncoding()))
            {
               foreach (string element in info)
               {
                  s_writer.WriteLine(element);
               }
            }
         }
      }
   }
}