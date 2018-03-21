using System;
using ISourceFilesLibrary.Classes.CodeFragment;

namespace IClone.ICloneReport.Classes
{
   /// <summary>
   /// Два связанных фрагмента дублирующегося кода в одном или разных файлах
   /// </summary>
   public sealed class CRelatedCloneFragments
   {
      private CCodeFragment m_FirstFragment;
      private CCodeFragment m_SecondFragment;
      private CSimilarity m_Similarity;

      /// <summary>
      /// 
      /// </summary>
      /// <param name="_FirstFragment"></param>
      /// <param name="_SecondFragment"></param>
      public CRelatedCloneFragments(CCodeFragment _FirstFragment, CCodeFragment _SecondFragment)
      {
         if (_FirstFragment == null || _SecondFragment == null)
         {
            throw new ArgumentNullException();
         }
         m_FirstFragment = _FirstFragment;
         m_SecondFragment = _SecondFragment;
         m_Similarity = new CSimilarity();
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="_FirstFragment"></param>
      /// <param name="_SecondFragment"></param>
      /// <param name="_Similarity"></param>
      public CRelatedCloneFragments(CCodeFragment _FirstFragment, CCodeFragment _SecondFragment, CSimilarity _Similarity)
      {
         if (_FirstFragment == null || _SecondFragment == null || _Similarity == null)
         {
            throw new ArgumentNullException();
         }
         m_FirstFragment = _FirstFragment;
         m_SecondFragment = _SecondFragment;
         m_Similarity = _Similarity;
      }

      /// <summary>
      /// Схожесть
      /// </summary>
      public CSimilarity Similarity
      {
         get
         {
            return m_Similarity;
         }

         set
         {
            m_Similarity = value;
         }
      }

      /// <summary>
      /// Первый дублирующийся фрагмент кода
      /// </summary>
      public CCodeFragment FirstFragment
      {
         get
         {
            return m_FirstFragment;
         }

         set
         {
            m_FirstFragment = value;
         }
      }

      /// <summary>
      /// Второй дублирующийся фрагмент кода
      /// </summary>
      public CCodeFragment SecondFragment
      {
         get
         {
            return m_SecondFragment;
         }

         set
         {
            m_SecondFragment = value;
         }
      }
   }
}