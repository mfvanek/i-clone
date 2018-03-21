using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICloneSearchLibrary.Classes.ClonedRowsMatrix;

namespace IClone.ICloneReport.Classes
{
    public class CSimpleCloneExtractor : CBaseCloneExtractor
    {
        protected override void BuildClonedFragmentsList(CBaseClonedRowsMatrix ClonedRowsMatrix)
        {
            if (ClonedRowsMatrix is CClonedRowsMatrix)
            {
            }
            else
            {
                throw new NotSupportedException("Данный тип матрицы клон-строк не поддерживается. Требуется CClonedRowsMatrix");
            }
        }
    }
}