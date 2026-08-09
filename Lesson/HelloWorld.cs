Vector3 vectorA = new Vector3(7, 3, -7);

Vector3 vectorB = new Vector3(8, 5, 4);

Vector3 vectorC = new Vector3(4, 2, 6.5);

Vector3 vectorD = new Vector3(8, -2, 9);
//This is a function (vectorsum)
Vector3 VectorSum(Vector3 anythingvectory, Vector3 anythingvectory2) {
	return new Vector3(anythingvectory.X + anythingvectory2.X, anythingvectory.Y + anythingvectory2.Y, anythingvectory.Z + anythingvectory2.Z);
}

//typewhateverIwant
//summing vectors with Vector3 struct
Vector3 vectorSum1 = VectorSum(vectorA, vectorB);
Vector3 vectorSum2 = VectorSum(vectorC, vectorD);
Vector3 vectorSum3 = VectorSum(vectorSum1, vectorSum2);
//bullshit I wrote earlier
Console.WriteLine("this shit sucks because they took away my ; but classes might get it back");
Console.WriteLine("note:{are always indented} ");

Console.WriteLine(vectorSum3);
//define Vector3
struct Vector3 {
	public double X;
	public double Y;
	public double Z;
//This is the constructor Vector3
	public Vector3(double x, double y, double z) {
		X = x;
		Y = y;
		Z = z;
	}
//override to print
    public override string ToString () {

        return $"{X}, {Y}, {Z}";
    }
}
