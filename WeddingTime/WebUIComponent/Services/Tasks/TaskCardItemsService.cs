using AIT.TaskDomain.Model.Enums;
using AIT.UtilitiesComponents.Strategy;
using System.Text.RegularExpressions;

namespace AIT.WebUIComponent.Services.Tasks
{
    public class TaskCardItemsService : ITaskCardItemsService
    {
        private IFunctionStrategyService<string, string, bool> _validationStrategy;
        private Regex _linkRegex = new Regex(@"^(http|https)://\S+$", RegexOptions.Compiled);
        private Regex _emailRegex = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", RegexOptions.Compiled);
        private Regex _phoneRegex = new Regex(@"^((\(\+[0-9]*\))?)((\([0-9]*\))?)((\+[0-9]*)?)[0-9 \-]*$", RegexOptions.Compiled);
        
        public TaskCardItemsService(IFunctionStrategyService<string, string, bool> validationStrategy)
        {
            _validationStrategy = validationStrategy;
            InitializeItemsStrategy();
        }

        private void InitializeItemsStrategy()
        {            
            _validationStrategy.AddStrategy(ItemType.Email.ToString(), ValidateEmail);
            _validationStrategy.AddStrategy(ItemType.Phone.ToString(), ValidatePhone);
            _validationStrategy.AddStrategy(ItemType.Link.ToString(), ValidateLink);
            _validationStrategy.AddStrategy(ItemType.ContactPerson.ToString(), val => { return true; });
            _validationStrategy.AddStrategy(ItemType.Address.ToString(), val => { return true; });
            _validationStrategy.SetDefaultStrategy(val => { return false; });
        }

        public bool Validate(string type, string value)
        {
            return _validationStrategy.Execute(type, value);
        }

        private bool ValidateEmail(string value)
        {
            return _emailRegex.IsMatch(value);
        }

        private bool ValidatePhone(string value)
        {
            return _phoneRegex.IsMatch(value);
        }

        private bool ValidateLink(string value)
        {
            return _linkRegex.IsMatch(value);
        }
    }
}