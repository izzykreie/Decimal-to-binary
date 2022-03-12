using System;

Console.WriteLine("Enter a number to convert to binary (format as wholeNumber.Decimal - For example, you could enter: 0.2 or 5.0 or -23.8): ");
string? testNumString = Console.ReadLine();
double testNumDub = double.Parse(testNumString);
int exponent = 0;

bool isNumNegative = false;
if (testNumDub < 0)
{
    isNumNegative = true;
}

if (isNumNegative)
{
    testNumDub = testNumDub * -1;
    testNumString = testNumDub.ToString();

    if (testNumDub % 1 == 0)
    {
        testNumString = testNumString + ".0";
    }
}

int indexOfDecimal = testNumString.IndexOf(".");

int WholeNumInput = Int32.Parse(testNumString.Substring(0, indexOfDecimal));
double DecimalInput = double.Parse(testNumString.Substring(indexOfDecimal));

string convertedWholeNum = convertWholeNumToBinary(WholeNumInput);
string convertedDecimalNum = convertDecimalToBinary(DecimalInput);

string joinedNum = convertedWholeNum + "." + convertedDecimalNum;

if (isNumNegative)
    joinedNum = joinedNum.Insert(0, "-");
else
    joinedNum = joinedNum.Insert(0, "+");

Console.WriteLine("");
Console.WriteLine("The number converted to binary is: " + joinedNum);
Console.WriteLine("");

string normalizedNum = normalize(convertedWholeNum, convertedDecimalNum, joinedNum, ref exponent);
finalizeAndPrint(normalizedNum, exponent);


string convertWholeNumToBinary(int numToConvert)
{
    string convertedBinWholeNum = "";

    while (numToConvert != 0)
    {
        int remainder = numToConvert % 2;
        numToConvert = numToConvert / 2;

        convertedBinWholeNum = convertedBinWholeNum.Insert(0, remainder.ToString());
    }

    return convertedBinWholeNum;
}

string convertDecimalToBinary(double decimalNumToConvert)
{
    string convertedBinDecimal = "";
    int numOfDecimalPlaces = 0;


    while (decimalNumToConvert != 1.0 && numOfDecimalPlaces < 7)
    {
        numOfDecimalPlaces++;
        decimalNumToConvert = decimalNumToConvert - Math.Truncate(decimalNumToConvert);
        decimalNumToConvert = decimalNumToConvert * 2;

        convertedBinDecimal = convertedBinDecimal + Math.Truncate(decimalNumToConvert).ToString();
    }

    return convertedBinDecimal;
}

string normalize(string wholeNum, string decimalNum, string joinedNum, ref int exponent)
{
    int currentDecimalPlace;
    int desiredDecimalPlace;


    if (wholeNum == "")
    {
        currentDecimalPlace = joinedNum.IndexOf(".");
        desiredDecimalPlace = joinedNum.IndexOf("1") + 1;

        exponent = currentDecimalPlace - desiredDecimalPlace + 1;

        joinedNum = joinedNum.Insert(desiredDecimalPlace, ".");
        joinedNum = joinedNum.Remove(currentDecimalPlace, 1);

        //Trims out leading zeros
        char tempSign = joinedNum[0];
        joinedNum = joinedNum.Remove(0, 1);
        double tempJoinedNum = double.Parse(joinedNum);
        joinedNum = tempJoinedNum.ToString();
        joinedNum = joinedNum.Insert(0, tempSign.ToString());
    }

    else
    {
        currentDecimalPlace = joinedNum.IndexOf(".");
        desiredDecimalPlace = joinedNum.IndexOf("1") + 1;

        exponent = currentDecimalPlace - desiredDecimalPlace;

        joinedNum = joinedNum.Insert(desiredDecimalPlace, ".");
        joinedNum = joinedNum.Remove(currentDecimalPlace + 1, 1);
    }

    Console.WriteLine("Normalization");
    Console.WriteLine("The normalized binary number is: " + joinedNum + " " + "* 2^" + exponent);
    Console.WriteLine("");

    return joinedNum;
}

void finalizeAndPrint(string normalizedNum, int exponent)
{
    string sign;
    string binaryExponent = "";

    if (normalizedNum[0] == '-')
        sign = "1";
    else
        sign = "0";

    string significand;
    significand = normalizedNum.Substring(3);

    if (significand.Length > 4)
    {
        significand = significand.Substring(0, 4);
    }

    if (exponent > 4 || exponent < -3)
    {
        Console.WriteLine("An overflow occured with the exponent. Please enter a smaller number");
        return;
    }

    exponent = exponent + 3;

    if (exponent == 0)
        binaryExponent = "0";
    else
        binaryExponent = convertWholeNumToBinary(exponent);

    int binaryExponentLength = binaryExponent.Length;
    int significandLength = significand.Length;

    while (binaryExponentLength < 3)
    {
        binaryExponent = binaryExponent.Insert(0, "0");
        binaryExponentLength++;
    }

    while (significandLength < 4)
    {
        significand = significand + "0";
        significandLength++;
    }

    Console.WriteLine("The final binary representation in excess 3 is: ");
    Console.WriteLine("Sign: " + sign);
    Console.WriteLine("Exponent: " + binaryExponent);
    Console.WriteLine("Significand: " + significand);

    Console.WriteLine("");
    Console.WriteLine("The final binary number is: " + sign + " " + binaryExponent + " " + significand);
}