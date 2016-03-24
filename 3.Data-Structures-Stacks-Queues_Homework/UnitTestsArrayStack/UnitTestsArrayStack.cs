namespace UnitTestsArrayStack
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ArrayBasedStack;

    [TestClass]
    public class UnitTestsArrayStack
    {
        [TestMethod]
        public void CreateNewArrayStack_ShouldCreateEmtpyStack()
        {
            // Arrange + Act
            var arrayStack = new ArrayStack<int>();

            //Assert
            Assert.AreEqual(0, arrayStack.Count);
        }

        [TestMethod]
        public void PushElementInEmptyStack_ShouldReturnCount1()
        {
            // Arrange
            var arrayStack = new ArrayStack<int>();

            // Act
            arrayStack.Push(5);

            // Assert
            Assert.AreEqual(1, arrayStack.Count);
        }

        [TestMethod]
        public void PushPopElementFromStack_ShouldReturnTheElement()
        {
            // Arange
            var arrayStack = new ArrayStack<int>();

            // Act
            arrayStack.Push(5);
            var element = arrayStack.Pop();

            // Assert
            Assert.AreEqual(5, element);
            Assert.AreEqual(0, arrayStack.Count);
        }

        [TestMethod]
        public void Push1000Elements_ShouldIncreaseCountCorrectly_ShouldAutogrowWhenNecessary()
        {
            // Arange
            var arrayStack = new ArrayStack<string>();

            // Act + Assert
            Assert.AreEqual(0, arrayStack.Count);
            for (int i = 1; i <= 1000; i++)
            {
                arrayStack.Push("str-" + i);
                Assert.AreEqual(i, arrayStack.Count);
            }
        }

        [TestMethod]
        public void Pop1000Elements_ShouldReturnCorrectElement_ShouldDecreaseCountCorrectly()
        {
            // Arange
            var arrayStack = new ArrayStack<string>();

            // Act
            for (int i = 0; i < 1000; i++)
            {
                arrayStack.Push("str-" + i);
            }

            // Assert
            for (int i = 1000; i < 0; i++)
            {
                var element = arrayStack.Pop();
                string expectedElement = "str-" + i;
                Assert.AreEqual(expectedElement, element);
                Assert.AreEqual(i - 1, arrayStack.Count);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PopFromEmtpyStack_ThrowsException()
        {
            // Arange
            var arrayStack = new ArrayStack<int>();

            // Act
            arrayStack.Pop();
        }

        [TestMethod]
        public void CreatestackWithCutomeInitialCapacity_ShouldWorkCoerrectly()
        {
            // Arange
            var arrayStack = new ArrayStack<int>(1);

            // Act + Assert
            Assert.AreEqual(0, arrayStack.Count);
            arrayStack.Push(10);
            Assert.AreEqual(1, arrayStack.Count);
            arrayStack.Push(20);
            Assert.AreEqual(2, arrayStack.Count);

            var element1 = arrayStack.Pop();
            Assert.AreEqual(20, element1);
            Assert.AreEqual(1, arrayStack.Count);

            var element2 = arrayStack.Pop();
            Assert.AreEqual(10, element2);
            Assert.AreEqual(0, arrayStack.Count);
        }

        [TestMethod]
        public void ToArrayMethod_ShouldReturnArrayWithReversedElements()
        {
            // Arange
            var arrayStack = new ArrayStack<int>();

            // Act
            arrayStack.Push(3);
            arrayStack.Push(5);
            arrayStack.Push(-2);
            arrayStack.Push(7);
            var stackToArr = arrayStack.ToArray();

            // Assert 
            CollectionAssert.AreEqual(stackToArr, new int[4] { 7, -2, 5, 3 });
        }

        [TestMethod]
        public void EmptyStackToArray_ShouldReturnEmptyArray()
        {
            // Arange
            var arrayStack = new ArrayStack<DateTime>();

            // Act
            var stackToArray = arrayStack.ToArray();

            // Assert
            Assert.AreEqual(0, stackToArray.Length);
        }
    }
}
