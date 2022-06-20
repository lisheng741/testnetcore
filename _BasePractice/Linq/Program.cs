
var listA = new List<int>() { 1,2,3,4,5,6};
var listB = new List<int>() { 4, 5, 6, 7, 8 };

// 差集 A - B ： 剔除 A 中与 B 公共的部分
var a_b = listA.Except(listB).ToList();

Console.ReadLine();
