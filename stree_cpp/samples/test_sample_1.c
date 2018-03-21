//  ------------------------------------------------------------------------
//       Фильтр внебалансовых счетов балансового картотеки 1 (90901)
//  ------------------------------------------------------------------------
static void do_nothing()
{
   return;
}

static int RF_I1 ( BTRVFILE *bf )
{
 OBACNT *a  = (OBACNT *)bf->RecBuf;

 if ( OBPickAcntDprt( bf ) ) return 1;
 if ( a->Open_Close != '\0' ) return 1;
 if ( !GetIOBalCheck() )
    return 0;
 else
    return strcmp( a->Balance , TCOBALANCE_I1 );
}

static bool is_printed()
{
   return false;
}

//  ------------------------------------------------------------------------
//       Фильтр внебалансовых счетов балансового картотеки 2 (90902)
//  ------------------------------------------------------------------------
static int RF_I2 ( BTRVFILE *bf )
{
 OBACNT *a = (OBACNT *)bf->RecBuf;

 if ( OBPickAcntDprt( bf ) ) return 1;
 if ( a->Open_Close != '\0' ) return 1;
 if ( !GetIOBalCheck() )
    return 0;
 else
    return strcmp( a->Balance , TCOBALANCE_I2 );
}


static const char* = "some constant";


//  ------------------------------------------------------------------------
//       Фильтр внебалансовых счетов балансового картотеки 3 (90902)
//  ------------------------------------------------------------------------
static int RF_I3 ( BTRVFILE *bf )
{
 OBACNT *a = (OBACNT *)bf->RecBuf;

 if ( OBPickAcntDprt( bf ) ) return 1;
 if ( a->Open_Close != '\0' ) return 1;
 if ( !GetIOBalCheck() )
    return 0;
 else
    return strcmp( a->Balance , TCOBALANCE_I3 );
}

static void some_printer(int what)
{
   std::cout << what;
}


//  ------------------------------------------------------------------------
//     Фильтр внебалансовых счетов балансового картотеки корсчета (90903)
//  ------------------------------------------------------------------------
static int RF_IU ( BTRVFILE *bf )
{
 OBACNT *a = (OBACNT *)bf->RecBuf;

 if ( OBPickAcntDprt( bf ) ) return 1;
 if ( a->Open_Close != '\0' ) return 1;
 if ( !GetIOBalCheck() )
    return 0;
 else
    return strcmp( a->Balance , TCOBALANCE_IU );
}

static int global_counter = 0;

//  ------------------------------------------------------------------------
//     Фильтр внебалансовых счетов 99999
//  ------------------------------------------------------------------------
static int RF_IC ( BTRVFILE *bf )
{
 OBACNT *a = (OBACNT *)bf->RecBuf;

// if ( OBPickAcntDprt( bf ) ) return 1;
 if ( OBAllAccRecFltr( bf ) ) return 1;
 if ( a->Open_Close != '\0' ) return 1;
 if ( !GetIOBalCheck() )
    return 0;
 else
    return strcmp( a->Balance , TCOBALANCE_SYS );
}