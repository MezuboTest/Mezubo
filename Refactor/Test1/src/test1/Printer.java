package test1;

public class Printer { 
    
 public static void main(String[] args) {
     Config config = new Config();
     Prime prime = new Prime();
     int primeLst[] = new int[config.count + 1];

       primeLst = prime.generatePrime();
      

        int PAGENUMBER = 1;
        int PAGEOFFSET = 1;
        while (PAGEOFFSET <= config.count) {
            System.out.print("The First ");
            System.out.print(Integer.toString(config.count));
            System.out.print(" Prime Numbers === Page ");
            System.out.print(Integer.toString(PAGENUMBER));
            System.out.println("\n");
            for (int ROWOFFSET = PAGEOFFSET; ROWOFFSET <= PAGEOFFSET + config.rows - 1; ROWOFFSET++) {
                for (int columna = 0; columna <= config.columns - 1; columna++) {
                    if (ROWOFFSET + columna * config.rows <= config.count) {
                        System.out.printf("%10d", primeLst[ROWOFFSET + columna * config.rows]);
                    }
                }
                System.out.println();
            }
            System.out.println("\f");
            PAGENUMBER++;
            PAGEOFFSET += config.rows * config.columns;
        }
        
        
        
        
    }
 
    
   
}
