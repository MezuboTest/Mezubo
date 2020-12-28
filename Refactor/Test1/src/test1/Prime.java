
package test1;

public class Prime {

    
    public int[] generatePrime() {
        Config config = new Config();
        final int ORDMAX = 30;
        int primeLst[] = new int[config.count + 1];       

        int number = 1;
        int primeCount = 1;
        boolean isPrime;
        int ORD = 2;
        int SQUARE = 9;
        int N;
        int MULT[] = new int[ORDMAX + 1];
        primeLst[1] = 2;

        while (primeCount < config.count) {

            do {
                number += 2;
                if (number == SQUARE) {
                    ORD++;
                    SQUARE = primeLst[ORD] * primeLst[ORD];
                    MULT[ORD - 1] = number;
                    System.out.println("");
                    System.out.println("P[ORD]:" + primeLst[ORD]);
                    System.out.println("SQUARE:" + SQUARE);
                    for (int i = 0; i < 30; i++) {
                        System.out.print(" : " + MULT[i]);

                    }

                }

                N = 2;
                isPrime = true;
                while (N < ORD && isPrime) {
                    while (MULT[N] < number) {
                        MULT[N] += primeLst[N] + primeLst[N];
                    }
                    if (MULT[N] == number) {
                        isPrime = false;
                    }
                    N++;
                }
            } while (!isPrime);

            primeCount++;
            primeLst[primeCount] = number;
        }
        return primeLst;
    }
}
