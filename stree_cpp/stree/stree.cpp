#include <stdio.h>
#include <tchar.h>
#include <conio.h>

#include "TestHarness.h"

int _tmain(int, _TCHAR*)
{
   TestResult tr;
   TestRegistry::runAllTests(tr);

   _getch();
   return 0;
}