namespace TrackerLibrary.Models
{
    public class PersonModel
    {

        public PersonModel()
        {
            
        }
        public PersonModel(string firstNameValue, string lastNameValue, string emailValue, string cellphoneValue)
        {
            FirstName = firstNameValue;
            LastName = lastNameValue;
            EmailAddress = emailValue;
            CellPhoneNumber = cellphoneValue;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string CellPhoneNumber { get; set; }

        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}