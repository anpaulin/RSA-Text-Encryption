//By Ariel Paulin                                                                          //
//Acknowledgements:                                                                        //
//1) http://www.protagonize.com/work/titanic-essay                                         //
//Sample Text about Titanic was used as random text                                        //
//                                                                                         //
//2) http://eli.thegreenplace.net/2009/03/28/efficient-modular-exponentiation-algorithms/  //
//Advice for Repeated squaring Method                                                      //
/////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

class Bonus
{

    static void Main()
    {
        ulong P = 51839;
        ulong Q = 51913;
        ulong N = P*Q;
        
        ulong E = 1073741813;
        
        ulong D = 2138488109;
        
        ulong PhiN = (P-1)*(Q-1);
        
        ulong EPrime = 52741219;
        ulong NPrime = 3125033603;
       
       
       //String to Encrypt
       string Text = "Hello D.E.A.R. I am student 20509293. This is my little secret: 51839 and 51913. Not so far from New York, Titanic ignored iceberg warnings and blazed onward, struck an iceberg, then sank less than three hours later in the North Atlantic. This marked the death of the Unsinkable ship and the beginning of new safety precautions for sea travel. The Titanic, also known to be the greatest ship made in the history of ships. I LOVE ECE 103, I LOVE ECE 103, I LOVE ECE 103, I LOVE ECE 103, I LOVE ECE 103. WATER WATER WATER --- LOO LOO LOO. Waterloo ROCKS!!!!!...."; 
       
       //Split String into 4 Character Chunks
       string[] SplitText = SplitInChunks(Text, 4).ToArray();
       
       StreamWriter inStream = new StreamWriter("20509293.txt");
       inStream.Write(String.Format("{0} {1} {2} ", N, E, SplitText.GetLength(0)));
       
       foreach(string s in SplitText)
       {
            inStream.Write(String.Format("{0} ", Encrypt(ConvertedBits(s), D, N, EPrime, NPrime)));
            inStream.Flush();
       }
       
       Console.WriteLine("20509293.txt Has Been Created With Encrypted Text");
       
      
    }
    
 // Encrypt A Specified ulong List of ASCII into a ulong
    static public ulong Encrypt (List<ulong> ASCII, ulong Exponent, ulong Mod, ulong GivenE, ulong GivenN)
    {
        
        ulong[] LetterASCII = ASCII.ToArray();
        int count = 3;
        
        ulong EncodedText = 0;
        
        ulong EncryptedText = 0;
        
        ulong DoubleEncrypted = 0;
        
        EncodedText = Convert.ToUInt64(LetterASCII[count-3]*Math.Pow(256, count) + LetterASCII[count-2]*Math.Pow(256, count-1) + LetterASCII[count-1]*Math.Pow(256, count-2) + LetterASCII[count]);
        
        //Calculate the Encrypted Z value
        EncryptedText = ModPowQuickCalc(EncodedText, Exponent, Mod);
        

        //Calculate the Encrypted C value based on n' and e' given by Xavier
        DoubleEncrypted = ModPowQuickCalc(EncryptedText, GivenE, GivenN);    
        
        return DoubleEncrypted;
    }
    
//Calculate Big Mods Using Repeated Squaring
       static public ulong ModPowQuickCalc(ulong BaseValue, ulong exponent, ulong mod) 
       {
        if (exponent == 0)
        {
          return 1;
        }
        ulong RecursiveValue = ModPowQuickCalc(BaseValue, exponent/2, mod);  // Recursive Function Calling to Eliminate Overflow of ULong
        ulong final = (RecursiveValue*RecursiveValue) % mod;
        if (exponent % 2 == 1)
        {
           final = (final * BaseValue) % mod;
        }
        return final;
        }
    
//Create ulong List of Each Character Converted to ASCII
    static public List<ulong> ConvertedBits (string bitChunk)
    {
        
        List<ulong> bitASCII = new List<ulong>();
        
        char[] tmp = bitChunk.ToCharArray();
    
        string[] bitCHAR = new string[4];
        
        for(int i = 0; i < 4; i++)
        {
            bitCHAR[i] = tmp[i].ToString();
        }
        
        
        for (int i = 0; i < 4; i++)
        {
            bitASCII.Add(Convert.ToUInt64(ConvertToASCII(bitCHAR[i])));
        }
        return bitASCII;
    }
//Split a String By A Specified Size
    static public string[] SplitInChunks(string Text, int ChunkSize)
    {
        int ArrayLength = 0;
        ArrayLength = Text.Length/4;
        
        string[] TextinChunk = new string[ArrayLength];
        
        int StartIndex = 0;
       
        for(int i = 0; StartIndex < Text.Length; i++)
        {
            TextinChunk[i] = Text.Substring(StartIndex, 4);
            StartIndex = StartIndex + 4;
        }
        
        return TextinChunk;
    }
//Convert A Given String to ASCII String
    static public string ConvertToASCII(string text)
    {
        string ASCIIvalue = null;

        byte[] ASCII = Encoding.ASCII.GetBytes(text);

        foreach (byte b in ASCII)
        {
            ASCIIvalue += b;
        }
        return ASCIIvalue;
    }
}

