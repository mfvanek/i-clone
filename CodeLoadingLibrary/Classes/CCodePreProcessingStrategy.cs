using ICloneBaseLibrary.Classes;
using ISourceFilesLibrary.Classes.CodeUnitsCollection;

namespace CodeLoadingLibrary.Classes
{
   /// <summary>
   /// Абстрактный базовый класс для реализации различных способов предварительной обработки программного кода
   /// </summary>
   public abstract class CCodePreProcessingStrategy : CBreakableActions
   {
      /// <summary>
      /// Конструктор по умолчанию
      /// </summary>
      protected CCodePreProcessingStrategy()
      {
      }

      /// <summary>
      /// Название алгоритма предварительной обработки программного кода
      /// </summary>
      /// <returns></returns>
      public abstract string CodePreProcessingAlgorithmName();

      /// <summary>
      /// Выполнить предварительную обработку единиц кода
      /// </summary>
      /// <param name="PreProcessingOptions"></param>
      /// <param name="OriginalCodeUnitsCollection"></param>
      /// <returns></returns>
      public abstract CCodeUnitsCollection PreProcessing(CCodePreProcessingOptions PreProcessingOptions, CCodeUnitsCollection OriginalCodeUnitsCollection);

      /// <summary>
      /// Представить в виде строки
      /// </summary>
      /// <returns></returns>
      public override string ToString()
      {
         return CodePreProcessingAlgorithmName();
      }
   }
}