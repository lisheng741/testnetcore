
Class1 c1 = new Class1();

Class11 c11 = new Class11();

Class12 c12 = new Class12();

Console.WriteLine("c11 is Class1:" + (c11 is Class1));

Console.WriteLine("c11 is Class11:" + (c11 is Class11));

Console.WriteLine("c12 is Class1:" + (c12 is Class1));

Console.WriteLine("c12 is Class11:" + (c12 is Class11));

Class1 n1 = c1 as Class1;

Class1 n11 = c11 as Class1;
Class1 n011 = c11 as Class11;

Class1 n12 = c12 as Class1;
Class1 n012 = c12 as Class12;

Class11? nc11 = c1 as Class11;

Class12? nc12 = c1 as Class12;

// ----------------------------------------

Class1 pc = new Class1();

Class1 pc1 = new Class11();
Console.WriteLine(pc1.GetType());

Class1 pc2 = new Class12();
Console.WriteLine(pc2.GetType());

Console.ReadLine();
