using PigeonCms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System;
using System.Collections.Generic;

namespace PigeonCms.Test
{
    
    
    /// <summary>
    ///Classe di test per ItemTest.
    ///Creata per contenere tutti gli unit test ItemTest
    ///</summary>
    [TestClass()]
    public class ItemTest
    {



        /// <summary>
        ///Test per DeleteFiles
        ///</summary>
        [TestMethod()]
        public void DeleteFilesTest()
        {
            Item target = new Item(); // TODO: Eseguire l'inizializzazione a un valore appropriato
            target.DeleteFiles();
            Assert.Inconclusive("Impossibile verificare un metodo che non restituisce valori.");
        }

        /// <summary>
        ///Test per DeleteImages
        ///</summary>
        [TestMethod()]
        public void DeleteImagesTest()
        {
            Item target = new Item(); // TODO: Eseguire l'inizializzazione a un valore appropriato
            target.DeleteImages();
            Assert.Inconclusive("Impossibile verificare un metodo che non restituisce valori.");
        }
    }
}
