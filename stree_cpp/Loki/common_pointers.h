#pragma once

#include "loki\SmartPtr.h"

template<typename T>
struct DefSmartPtr
{
   typedef typename Loki::SmartPtr<T, Loki::RefLinked> Ptr;
};