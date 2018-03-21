using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICloneSearchLibrary.Classes.ClonedRowsMatrix;

namespace IClone.ICloneReport.Classes
{
    public class CHashBucketCloneExtractor : CBaseCloneExtractor
    {
        protected override void BuildClonedFragmentsList(CBaseClonedRowsMatrix ClonedRowsMatrix)
        {
            if (ClonedRowsMatrix is CClonedRowsContainer)
            {
                CClonedRowsContainer HashContainer = ClonedRowsMatrix as CClonedRowsContainer;
            }
            else
            {
                throw new NotSupportedException("Данный тип матрицы клон-строк не поддерживается. Требуется CClonedRowsContainer");
            }
        }
    }
}