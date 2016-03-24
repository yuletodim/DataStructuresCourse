namespace UnitTestsLinkedQueue
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using LinkedQueue;

    [TestClass]
    public class UnitTestsLinkedList
    {
        [TestMethod]
        public void CreateNewLinkedList_ShouldCreateEmptyQueue()
        {
            // Arange
            var linkedQueue = new LinkedQueue<int>();

            // Assert
            Assert.AreEqual(0, linkedQueue.Count);
        }

        [TestMethod]
        public void EnqueElements_ShouldIncreaseCountCorrectly()
        {
            // Arange
            var linkedQueue = new LinkedQueue<int>();

            // Act + Assert
            for (int i = 1; i <= 10; i++)
            {
                linkedQueue.Enqueque(i);
                Assert.AreEqual(i, linkedQueue.Count);
            }
        }

        [TestMethod]
        public void DequeElement_ShouldReturnTheElement_ShouldDecreaseCount()
        {
            // Arange
            var linkedQueue = new LinkedQueue<int>();

            // Act
            linkedQueue.Enqueque(10);
            var element = linkedQueue.Dequeue();

            // Assert
            Assert.AreEqual(10, element);
            Assert.AreEqual(0, linkedQueue.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DequeueEmptyQueue_ShouldThrowException()
        {
            // Arange
            var linkedQueue = new LinkedQueue<int>();

            // Act
            linkedQueue.Dequeue();
        }

        [TestMethod]
        public void EnqueueDequeue1000Elements_ShouldWorkCoerrectly()
        {
            // Arange
            var linkedQueue = new LinkedQueue<string>();
            int repeats = 1000;

            // Act & Assert in 2 loops
            for (int i = 1; i <= repeats; i++)
            {
                string str = "str-" + i;
                linkedQueue.Enqueque(str);
                Assert.AreEqual(i, linkedQueue.Count);
            }

            for (int i = 1; i <= repeats; i++)
            {
                repeats--;
                var element = linkedQueue.Dequeue();
                var expectedElement = "str-" + i;
                Assert.AreEqual(expectedElement, element);
                Assert.AreEqual(repeats, linkedQueue.Count);
            }
        }

        [TestMethod]
        public void ConvertEmpytQueueToArray_ShouldReturnEmptyArray()
        {
            // Arange
            var linkedQueue = new LinkedQueue<string>();

            // Act
            var queueAsArray = linkedQueue.ToArray();

            //Assert
            Assert.AreEqual(0, queueAsArray.Length);
        }

        [TestMethod]
        public void ConvertQueueToArray_ShouldReturnArrayWithCorrectLengthAndItems()
        {
            // Arange
            var arrayNumbers = Enumerable.Range(1, 500).ToArray();
            var linkedQueue = new LinkedQueue<int>();

            // Act
            for (int i = 0; i < 500; i++)
            {
                linkedQueue.Enqueque(arrayNumbers[i]);
            }

            var queueAsArray = linkedQueue.ToArray();

            //Assert
            CollectionAssert.AreEqual(arrayNumbers, queueAsArray);
        }
    }
}
