using System.ComponentModel.DataAnnotations;

namespace LaboratorioTestBack
{
    [TestClass]
    public sealed class CURPAttributeTest
    {
        //Paso 1: Preparar 
        //Paso 2: Probar 
        //Paso 3: Verificar

        [TestMethod]
        [DataRow("")]
        [DataRow("   ")]
        [DataRow(null)]
        public void IsError_IsEmply(string value)
        {
            //regredar incorrecto con un valor null o vacio 

            //Paso 1: Preparar 
            var curpAttribute = new LaboratorioBack.Validations.CURPAttribute();
            var validationContext = new ValidationContext(new object());
            //var validValue = "GODE561231HDFRRN09"; // Example of a valid NSS value

            //Paso 2: Probar 
            var result = curpAttribute.GetValidationResult(value, validationContext);

            //Paso 3: Verificar
            Assert.AreEqual(expected: "La CURP es requerida.", actual: result!.ErrorMessage);
        }

        [TestMethod]
        [DataRow("GODEA61231HDFRRN09")]

        public void IsError_WrongFormat(string value)
        {
            //regresar con un formato incorrecto

            //Paso 1: Preparar 
            var curpAttribute = new LaboratorioBack.Validations.CURPAttribute();
            var validationContext = new ValidationContext(new object());

            //Paso 2: Probar 
            var result = curpAttribute.GetValidationResult(value, validationContext);

            //Paso 3: Verificar
            Assert.AreEqual(expected: "El formato de la CURP es incorrecto.", actual: result!.ErrorMessage);
        }

        [TestMethod]
        [DataRow("GODE561231HDFRRN09")]
        public void IsValid_IsCorrect(string validValue)
        {
            //debe regresar valor exitoso con un valor requerido y correcto

            //Paso 1: Preparar 
            var curpAttribute = new LaboratorioBack.Validations.CURPAttribute();
            var validationContext = new ValidationContext(new object());
           
            //Paso 2: Probar 
            var result = curpAttribute.GetValidationResult(validValue, validationContext);

            //Paso 3: Verificar
            Assert.AreEqual(ValidationResult.Success, result);
        }
    }
}