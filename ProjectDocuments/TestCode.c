/* adler32.c -- compute the Adler-32 checksum of a data stream
 * Copyright (C) 1995-2004 Mark Adler
 * For conditions of distribution and use, see copyright notice in zlib.h
 */

/* @(#) $Id$ */

/* NMAX is the largest n such that 255n(n+1)/2 + (n+1)(BASE-1) <= 2^32-1 */
/* ========================================================================= */
uLong ZEXPORT adler32(uLong adler, const Bytef *buf, uInt len)
{
    unsigned long sum2;
    unsigned n;

    /* initial Adler-32 value (deferred check for len == 1 speed) */
    if (buf == Z_NULL)
        return 1L;

    /* do length NMAX blocks -- requires just one modulo operation */
    while (len >= NMAX) {
        len -= NMAX;
        n = NMAX / 16;          /* NMAX is divisible by 16 */
        do {
            DO16(buf);          /* 16 sums unrolled */
            buf += 16;
        } while (--n);
        MOD(adler);
        MOD(sum2);
    }

	//*
 int       a =          5; // not commented
//*/

/*
int            a = 5; // commented
//*/

    /* return recombined sums */
    return adler | (sum2 << 16);
}

/* ========================================================================= */