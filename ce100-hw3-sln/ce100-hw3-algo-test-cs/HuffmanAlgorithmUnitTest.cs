using System;
using System.Collections;
using System.IO;
using Xunit;
using ce100_hw3_algo_lib_cs;
using static System.Net.WebRequestMethods;


namespace ce100_hw3_algo_test_cs
{
    public class HuffmanAlgorithmTests
    {
        [Fact]
        public void EncodeAndDecode_Text_Success()
        {
            
            string inputText = "Hello, world!";
            HuffmanAlgorithm.HuffmanTree tree = new HuffmanAlgorithm.HuffmanTree();

            
            tree.Build(inputText);
            var encodedBits = tree.Encode(inputText);
            var decodedText = tree.Decode(encodedBits);

            
            Assert.Equal(inputText, decodedText);
        }

        [Fact]
        public void EncodeAndDecode_MP3_Success()
        {
            
            byte[] inputBytes = { 0x48, 0x45, 0x4C, 0x4C, 0x4F, 0x20, 0x57, 0x4F, 0x52, 0x4C, 0x44, 0x21 };
            HuffmanAlgorithm.HuffmanTree_mp3 tree = new HuffmanAlgorithm.HuffmanTree_mp3();

            
            tree.Build(inputBytes);
            var encodedBits = tree.Encode(inputBytes);
            var decodedBytes = tree.Decode(encodedBits);

            
            Assert.Equal(inputBytes, decodedBytes);
        }
    }
}