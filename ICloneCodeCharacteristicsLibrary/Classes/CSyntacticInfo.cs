using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICloneCodeCharacteristicsLibrary.Classes
{
   /// <summary>
   /// Основные синтаксические характеристики кода
   /// </summary>
   public sealed class CSyntacticInfo
   {
      DateTime m_Date;
      long m_MinSyntacticUnitSize;
      long m_MaxSyntacticUnitSize;
      long m_TotalSyntacticUnitsCount;
      long m_TotalCodeUnitsCount;
      double m_MediumSyntacticUnitSize;
      double m_Dispersion;
      double m_MeanSquareDeviation;
      double m_gamma;
      long m_Kmin;

      static double m_Ic = 0.9999;

      #region

      /// <summary>
      /// 
      /// </summary>
      public CSyntacticInfo()
      {
         m_Date = DateTime.Now;
         m_MinSyntacticUnitSize = 0;
         m_MaxSyntacticUnitSize = 0;
         m_TotalSyntacticUnitsCount = 0;
         m_TotalCodeUnitsCount = 0;
         m_MediumSyntacticUnitSize = 0;
         m_Dispersion = 0;
         m_MeanSquareDeviation = 0;
         m_gamma = m_Ic;
         m_Kmin = 0;
      }

      /// <summary>
      /// Дата выполнения операции
      /// </summary>
      public DateTime Date
      {
         get
         {
            return m_Date;
         }

         set
         {
            m_Date = value;
         }
      }

      /// <summary>
      /// Размер наименьшей синтаксической единицы кода
      /// </summary>
      public long MinSyntacticUnitSize
      {
         get
         {
            return m_MinSyntacticUnitSize;
         }

         set
         {
            if (value > 0)
               m_MinSyntacticUnitSize = value;
            else
               throw new ArgumentOutOfRangeException("MinSyntacticUnitSize");
         }
      }

      /// <summary>
      /// Размер наибольшей синтаксической единицы кода
      /// </summary>
      public long MaxSyntacticUnitSize
      {
         get
         {
            return m_MaxSyntacticUnitSize;
         }

         set
         {
            if (value > 0)
               m_MaxSyntacticUnitSize = value;
            else
               throw new ArgumentOutOfRangeException("MaxSyntacticUnitSize");
         }
      }

      /// <summary>
      /// Общее количество синтаксических единиц кода
      /// </summary>
      public long TotalSyntacticUnitsCount
      {
         get
         {
            return m_TotalSyntacticUnitsCount;
         }

         set
         {
            if (value > 0)
               m_TotalSyntacticUnitsCount = value;
            else
               throw new ArgumentOutOfRangeException("TotalSyntacticUnitsCount");
         }
      }

      /// <summary>
      /// Общее количество единиц кода
      /// </summary>
      public long TotalCodeUnitsCount
      {
         get
         {
            return m_TotalCodeUnitsCount;
         }

         set
         {
            m_TotalCodeUnitsCount = value;
         }
      }

      /// <summary>
      /// Средний размер синтаксической единицы кода
      /// </summary>
      public double MediumSyntacticUnitSize
      {
         get
         {
            return m_MediumSyntacticUnitSize;
         }

         set
         {
            if (value > 0)
               m_MediumSyntacticUnitSize = value;
            else
               throw new ArgumentOutOfRangeException("MediumSyntacticUnitSize");
         }
      }

      private void calc_Kmin()
      {
         if (m_MinSyntacticUnitSize < m_MediumSyntacticUnitSize)
            m_gamma = m_Ic * (1 - (m_MinSyntacticUnitSize / m_MediumSyntacticUnitSize));
         else
            m_gamma = m_Ic;

         m_Kmin = (long)Math.Round(m_gamma * (m_MediumSyntacticUnitSize - m_MeanSquareDeviation));
      }

      /// <summary>
      /// Дисперсия
      /// </summary>
      public double Dispersion
      {
         get
         {
            return m_Dispersion;
         }

         set
         {
            if (value >= 0)
            {
               m_Dispersion = value;
               m_MeanSquareDeviation = Math.Sqrt(m_Dispersion);
               calc_Kmin();
            }
            else
               throw new ArgumentOutOfRangeException("Dispersion");
         }
      }

      /// <summary>
      /// Среднеквадратическое отклонение
      /// </summary>
      public double MeanSquareDeviation
      {
         get
         {
            return m_MeanSquareDeviation;
         }
      }

      /// <summary>
      /// пороговое значение размера фрагмента кода
      /// </summary>
      public long Kmin
      {
         get
         {
            return m_Kmin;
         }
      }
      
      #endregion
   }
}
