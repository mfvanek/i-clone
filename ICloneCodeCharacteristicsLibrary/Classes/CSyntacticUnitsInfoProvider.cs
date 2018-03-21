using System;
using System.Threading;
using CodeLoadingLibrary.Classes;
using ICloneExtensions;
using ICloneExtensions.Classes;
using ICloneExtensions.Interfaces;
using ISourceFilesLibrary.Classes.Languages;
using ISourceFilesLibrary.Classes.SourceFile;
using ISourceFilesLibrary.Classes.SyntacticUnit;

namespace ICloneCodeCharacteristicsLibrary.Classes
{
   /// <summary>
   /// Класс для получения сводной информации о статистических характеристиках исходного кода
   /// </summary>
   public class CSyntacticUnitsInfoProvider : CLoadFilesProcessor
   {
      private CSyntacticUnitsCollection m_SynUnitsCollection;
      private CSyntacticInfo info;
      /// <summary>
      /// Языковой плагин
      /// </summary>
      protected ICloneExtension m_Extension;
      /// <summary>
      /// Количество найденных синтаксических единиц
      /// </summary>
      protected long m_CountOfSyntacticUnits;

      /// <summary>
      /// 
      /// </summary>
      /// <param name="LanguageID"></param>
      /// <param name="load_files_options"></param>
      public CSyntacticUnitsInfoProvider(LANGUAGES LanguageID, CLoadFilesOptions load_files_options)
         : base(load_files_options)
      {
         m_Extension = CAvailableExtentions.GetExtention(LanguageID);
         m_SynUnitsCollection = new CSyntacticUnitsCollection();
         m_CountOfSyntacticUnits = 0;
      }

      /// <summary>
      /// Получить набор синтаксических единиц для заданного файла
      /// </summary>
      /// <param name="filename"></param>
      protected override void LoadOneFile(string filename)
      {
         CTokenizerParms args = new CTokenizerParms(filename, m_LoadOptions.FileEncoding, new CSourceFileID());
         CSyntacticUnitsCollection current_collection = m_Extension.Syntacticize(args);
         m_CountOfSyntacticUnits += current_collection.Size();
         //Interlocked.Add(ref m_CountOfSyntacticUnits, current_collection.Size());
         m_SynUnitsCollection.AddRange(current_collection);
      }

      bool IsAnythingFound()
      {
         return (m_SynUnitsCollection.Size() > 0);
      }

      private void calc_medium_size()
      {
         if (IsAnythingFound())
         {
            info.TotalSyntacticUnitsCount = m_SynUnitsCollection.Size();

            //long size_of_all_units = 0;
            if (m_SynUnitsCollection.Size() > 0)
               info.MinSyntacticUnitSize = m_SynUnitsCollection.front().Size();

            foreach (CSyntacticUnit unit in m_SynUnitsCollection)
            {
               info.TotalCodeUnitsCount += unit.Size();
               //size_of_all_units += unit.Size();
               if (unit.Size() < info.MinSyntacticUnitSize)
                  info.MinSyntacticUnitSize = unit.Size();
               if (unit.Size() > info.MaxSyntacticUnitSize)
                  info.MaxSyntacticUnitSize = unit.Size();
            }
            info.MediumSyntacticUnitSize = info.TotalCodeUnitsCount / info.TotalSyntacticUnitsCount;
         }
      }

      private void calc_dispersion()
      {
         if (IsAnythingFound())
         {
            double disp = 0;
            foreach (CSyntacticUnit unit in m_SynUnitsCollection)
            {
               disp += Math.Pow((unit.Size() - info.MediumSyntacticUnitSize), 2);
            }
            info.Dispersion = disp / info.TotalCodeUnitsCount;// info.TotalSyntacticUnitsCount;
         }
      }

      private void calc_metrics()
      {
         info = new CSyntacticInfo();
         calc_medium_size();
         calc_dispersion();
      }

      private void save()
      {
         try
         {
            CSyntacticInfoSaver saver = new CSyntacticInfoSaver(info, m_LoadOptions.Directory);
            saver.Save();
         }
         catch
         {
         }
      }

      /// <summary>
      /// Получить синтаксические характеристики кода
      /// </summary>
      /// <returns></returns>
      public virtual CSyntacticInfo Calculate()
      {
         m_SynUnitsCollection = new CSyntacticUnitsCollection();
         m_CountOfSyntacticUnits = 0;
         ProcessLoad();
         calc_metrics();
         save();
         return info;
      }
   }
}