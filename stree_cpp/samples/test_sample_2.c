
//
//               –абота с книгами покупок/продаж
//
bool BillBook_Mac( void *vd )
{
   STD_USERDATA *ud = (STD_USERDATA*)vd;
   PARM_PALYREP *md;

   if (FirstRSL) md = (PARM_PALYREP*)ud;
   else          md = (PARM_PALYREP*)ud->addExecData;
   char         Type = md->Type; 
   int          iReadOnly = md->iReadOnly;
   BSCROL      *Bs;
   PANEL       *PE, *PI, *PR;
   BILLBOOK    *bb;
   int          i;
   char         AccessToAllBB;
   int          key = 0;
   bdate        StDate, EnDate;

   if ( OpenBillFiles( &billf ) ) {
      berr( W1 );
      CloseBillFiles( &billf );
      md->err = bfstat;   
      return iFALSE;
   }
   p_num = -1;

   bb = (BILLBOOK*)  FileBillBook->RecBuf;

   FileBillRes->KeyNum = BILLRESULT_KEYBILLNUM;

   PARM_BILLBOOK parm;
   memset(&parm, 0, sizeof(PARM_BILLBOOK));
   parm.locReadOnly = iReadOnly;
   parm.BookType = Type;
   parm.ForceUpdate = OFF;
   SF_WinRepFlags  Exprt;
   Exprt.LoadWinRepFlags(Type == SALEBOOK ? WINREP_SV_SALE : WINREP_SV_BUY, &parm.ExprtFlags);

   if( CanUseNewFeature() )
   {
      char str[LEN_RegStringValue];
      if( GetRegValue( "BILLBOOK/ACCESSTOALLBB", REG_STRING, &str ) == REG_STRING_TYPE && str[0] )
         AccessToAllBB = SET_CHAR;
      else
         AccessToAllBB = UNSET_CHAR;
   }
   memset( &sf , 0 , sizeof( FILTERSF ) );
   sf.Date1 = sf.Date2 = curdate;
   sf.AccessToAllBB = AccessToAllBB;

   if ( ( Bs = CrBs( &AppRes , (Type == SALEBOOK ? "SALEBOOK":"COVRBOOK" ) ) ) != NULL ) {

      if ( ( PE = CrPan( &AppRes , (Type == SALEBOOK ? SALEPAN : COVRPAN ) ) ) != NULL ) {

         if ( ( PI = CrPan( &AppRes , (Type == SALEBOOK ? SALEPAN : COVRPAN ) ) ) != NULL ) {

            if( ( PR = CrPan( &AppRes, "BB_DATE") ) != NULL ) {

               // ”становить пол€ дл€ панели периода
               //       p   proc  Parm  Pstatus PHead
               SetFlds( PR, P_PR, NULL, NULL,   "ƒиапазон дат счетов-фактур",
                  &sf.Date1,
                  &sf.Date2 );


               DP_DEPARTMENT * dep = (DP_DEPARTMENT*) File_DP_Dep->RecBuf;

               SetFilterSF( parm.BookType , &sf );

               if ( Type == COVERINGBOOK ) {   // книга покупок
                  pf_NameClient = 7;
                  //        Bs  P_s  Parm  Stat  head  bfile          Mpe Mpi   n     i       c      d     make
                  BSetFlds( Bs, P_s, &parm, NULL, NULL, FileBillBook,  PE, PI, Init, CheckI , CheckE, Delete, make,
                     dep->name,
                     &parm.IsBillCorr, 
                     &parm.Bill_reg_numb,
                     parm.Bill_numb,
                     &parm.Bill_date,
                     &bb->GetDate,
                     &bb->PayDate,
                     &bb->FixDate,
                     bb->Name_Client,
                     bb->INN,
                     bb->KPP,
                     //bb->szOKONH, - #69600
                     bb->szOKPO,
                     &parm.CostWithNDS,
                     &NDS20,
                     &NDS10,
                     &NDS0
                     );

                  //       p   proc  Parm  Pstatus PHead
                  SetFlds( PE, P_e, Bs,   NULL,   NULL,
                     &bb->Bn.Bill_reg_numb,
                     bb->Bn.Bill_numb,
                     &bb->Bn.Bill_date,
                     &bb->GetDate,
                     &bb->PayDate,
                     &bb->FixDate,
                     dep->name,
                     bb->Name_Client,
                     bb->INN,
                     bb->KPP,
                     //bb->szOKONH, - #69600
                     bb->szOKPO,
                     &bb->CostWithNDS,
                     &NDS10,
                     &NDS20,
                     &NDS0
                     );

                  //       p   proc  Parm  Pstatus PHead
                  SetFlds( PI, P_i, Bs,   NULL,   NULL,
                     &bb->Bn.Bill_reg_numb,
                     bb->Bn.Bill_numb,
                     &bb->Bn.Bill_date,
                     &bb->GetDate,
                     &bb->PayDate,
                     &bb->FixDate,
                     dep->name,
                     bb->Name_Client,
                     bb->INN,
                     bb->KPP,
                     //bb->szOKONH, - #69600
                     bb->szOKPO,
                     &bb->CostWithNDS,
                     &NDS10,
                     &NDS20,
                     &NDS0
                     );

               }
               else {
                  pf_NameClient = 5;
                  //        Bs  P_s  Parm  Stat  head  bfile          Mpe Mpi   n     i       c      d     make
                  BSetFlds( Bs, P_s, &parm, NULL, NULL, FileBillBook,  PE, PI, Init, CheckI , CheckE, Delete, make,
                     dep->name,
                     &parm.IsBillCorr,
                     &parm.Bill_reg_numb,
                     parm.Bill_numb,
                     &parm.Bill_date,
                     bb->Name_Client,
                     bb->INN,
                     bb->KPP,
                     //bb->szOKONH, - #69600
                     bb->szOKPO,
                     &parm.CostWithNDS,
                     &NDS20,
                     &NDS10,
                     &NDS0,
                     &NDS0EXPORT
                     );

                  //       p   proc  Parm  Pstatus PHead
                  SetFlds( PE, P_e, Bs,   NULL,   NULL,
                     &bb->Bn.Bill_reg_numb,
                     bb->Bn.Bill_numb,
                     dep->name,
                     &bb->Bn.Bill_date,
                     &bb->ExhibitingDate,
                     bb->Name_Client,
                     bb->INN,
                     bb->KPP,
                     //bb->szOKONH, - #69600
                     bb->szOKPO,
                     &bb->CostWithNDS,
                     &NDS10,
                     &NDS20,
                     &NDS0,
                     &NDS0EXPORT
                     );

                  //       p   proc  Parm  Pstatus PHead
                  SetFlds( PI, P_i, Bs,   NULL,   NULL,
                     &bb->Bn.Bill_reg_numb,
                     bb->Bn.Bill_numb,
                     dep->name,
                     &bb->Bn.Bill_date,
                     &bb->ExhibitingDate,
                     bb->Name_Client,
                     bb->INN,
                     bb->KPP,
                     //bb->szOKONH, - #69600
                     bb->szOKPO,
                     &bb->CostWithNDS,
                     &NDS10,
                     &NDS20,
                     &NDS0,
                     &NDS0EXPORT
                     );

               }

               PI->Pstatus = InpPan; // F7 не нужно..
               PE->Pstatus = EdtPan; // F7    нужно..

               if ( iReadOnly )      {
                  Bs->Mp.Pstatus = BossBs;
                  PE->Pstatus = BossPn;
                  for ( i=0; i<PE->Pnumf; i++ )
                     PE->PFList[i].Ftype = FBT;
               }
               else
                  Bs->Mp.Pstatus = BsStatus;  


               if( !PeriodOfBillBook( PR ) )
               {
                  while( ON )
                  {
                     SetFilterSF( parm.BookType , &sf );

                     if( !iReadOnly ) 
                        key = BEditScrol(Bs); 
                     else
                        key = BLoopScrol(Bs); 
                     if( !( key == QUITSCRL  && IsFiltMod( Bs ) ) )
                        break;
                     fmeswin( MsgFile, 188 );
                     UnsetFiltMod( Bs );
                     StDate = sf.Date1;      //<- #102751
                     EnDate = sf.Date2;
                     memset( &sf , 0 , sizeof( FILTERSF ) );
                     sf.Date1 = StDate;
                     sf.Date2 = EnDate;      //<- #102751
                     sf.AccessToAllBB = AccessToAllBB;
                  }
               }


            }
            else NoMem();

            free( PI );

         }
         else NoMem();

         free( PE );

      }
      else NoMem();

      free( Bs );

   }
   else NoMem();

   CloseBillFiles( &billf );

   return iTRUE;
}