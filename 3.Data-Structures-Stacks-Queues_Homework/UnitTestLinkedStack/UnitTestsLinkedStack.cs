namespace UnitTestLinkedStack
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using LinkedStack;


    [TestClass]
    public class UnitTestsLinkedStack
    {
        [TestMethod]
        public void CreateNewLinkedStack_ShouldCreateEmtpyStack()
        {
            // Arrange + Act
            var linkedStack = new LinkedStack<int>();

            //Assert
            Assert.AreEqual(0, linkedStack.Count);
        }

        [TestMethod]
        public void PushElementInEmptyStack_ShouldReturnCount1()
        {
            // Arrange
            var linkedStack = new LinkedStack<int>();

            // Act
            linkedStack.Push(5);

            // Assert
            Assert.AreEqual(1, linkedStack.Count);
        }

        [TestMethod]
        public void PushPopElementFromStack_ShouldReturnTheElement()
        {
            // Arange
            var linkedStack = new LinkedStack<int>();

            // Act
            linkedStack.Push(5);
            var element = linkedStack.Pop();

            // Assert
            Assert.AreEqual(5, element.Value);
            Assert.AreEqual(0, linkedStack.Count);
        }

        [TestMethod]
        public void Push1000Elements_ShouldIncreaseCountCorrectly()
        {
            // Arange
            var linkedStack = new LinkedStack<string>();

            // Act + Assert
            Assert.AreEqual(0, linkedStack.Count);
            for (int i = 1; i <= 1000; i++)
            {
                linkedStack.Push("str-" + i);
                Assert.AreEqual(i, linkedStack.Count);
            }
        }

        [TestMethod]
        public void Pop1000Elements_ShouldReturnCorrectElement_ShouldDecreaseCountCorrectly()
        {
            // Arange
            var linkedStack = new LinkedStack<string>();

            // Act
            for (int i = 0; i < 1000; i++)
            {
                linkedStack.Push("str-" + i);
            }

            // Assert
            for (int i = 1000; i < 0; i++)
            {
                var element = linkedStack.Pop();
                string expectedElement = "str-" + i;
                Assert.AreEqual(expectedElement, element.Value);
                Assert.AreEqual(i - 1, linkedStack.Count);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PopFromEmtpyStack_ThrowsException()
        {
            // Arange
            var linkedStack = new LinkedStack<int>();

            // Act
            linkedStack.Pop();
        }

        [TestMethod]
        public void ToArrayMethod_ShouldReturnArrayWithReversedElements()
        {
            // Arange
            var linkedStack = new LinkedStack<int>();

            // Act
            linkedStack.Push(3);
            linkedStack.Push(5);
            linkedStack.Push(-2);
            linkedStack.Push(7);
            var stackToArr = linkedStack.ToArray();

            // Assert 
            CollectionAssert.AreEqual(stackToArr, new int[4] { 7, -2, 5, 3 });
        }

        [TestMethod]
        public void EmptyStackToArray_ShouldReturnEmptyArray()
        {
            // Arange
            var linkedStack = new LinkedStack<DateTime>();

            // Act
            var stackToArray = linkedStack.ToArray();

            // Assert
            Assert.AreEqual(0, stackToArray.Length);
        }
    }
}
