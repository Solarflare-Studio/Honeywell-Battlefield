using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Inition.HoneyWell.ParseData;
using System;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace Inition.ContactForm
{
    public class ContactFormManager : MonoBehaviour
    {
        [SerializeField]
        private string emailAddressInputFieldName = "EmailInputField";
        private InputField emailAddressInputField;

        [SerializeField]
        private string emailAddressExclamationName = "ExclamationMarkEmailText";
        private Text emailAddressExclamationText;

        [SerializeField]
        private string nameInputFieldName = "NameInputField";
        private InputField nameInputField;

        [SerializeField]
        private string jobTitleInputFieldName = "JobTitleInputField";
        private InputField jobTitleInputField;

        [SerializeField]
        private string infoFormSubmitButtonName = "InfoFormSubmitButton";
        private Button submitButton;

        [SerializeField]
        private string completedMessageName = "CompletedMessageTest";
        private Text completedMessage;
         
        [SerializeField]
        private string toggleHolderLeftColumnName = "TogglesColumnLeft";
        private RectTransform toggleHolderLeft;
        [SerializeField]
        private string toggleHolderRightColumnName = "TogglesColumnRight";
        private RectTransform toggleHolderRight;

        private List<ToggleElement> toggles = new List<ToggleElement>();
        [SerializeField]
        private string togglePrefabResourceName = "InfoFormTogglePrefab";

        [SerializeField]
        private string getMoreInfoTextName = "GetMoreInfoText";

        private bool valid = false;

        public ContactFormManager Instance { get; internal set; }

        public void Initialize(string _formMainProduct)
        {
            emailAddressInputField = transform.GetChildFromName<InputField>(emailAddressInputFieldName);
            nameInputField = transform.GetChildFromName<InputField>(nameInputFieldName);
            jobTitleInputField = transform.GetChildFromName<InputField>(jobTitleInputFieldName);
            completedMessage = transform.GetChildFromName<Text>(completedMessageName);

            emailAddressExclamationText = transform.GetChildFromName<Text>(emailAddressExclamationName);

            submitButton = transform.GetChildFromName<Button>(infoFormSubmitButtonName);
            submitButton.onClick.AddListener(() => SubmitButtonPressed());

            toggleHolderLeft = transform.parent.GetChildFromName<RectTransform>(toggleHolderLeftColumnName); 
            toggleHolderRight = transform.parent.GetChildFromName<RectTransform>(toggleHolderRightColumnName);

            transform.GetChildFromName<Text>(getMoreInfoTextName).text += _formMainProduct;

            PopulateToggleList(_formMainProduct);
        }

        void PopulateToggleList(string _mainProductTitle)
        {
            bool addLeft = true;
            DataObject[] data = LoadAndParseData.Instance.loadedDataObjects.ToArray();

            toggles = new List<ToggleElement>();
            for(int i = 0; i < data.Length; i++)
            {
                ToggleElement newElement;
                newElement = new ToggleElement(Instantiate(Resources.Load(togglePrefabResourceName) as GameObject).GetComponent<Toggle>(), data[i].markerID, data[i].markerName);

                string titleHolder = data[i].title;
                newElement.toggle.isOn = (_mainProductTitle == titleHolder);    //if the current title is the main title (that triggered the form) tick it..

                newElement.toggle.GetComponentInChildren<Text>().text = titleHolder;
                 

                if (addLeft) 
                    newElement.toggle.transform.SetParent(toggleHolderLeft);  
                else
                    newElement.toggle.transform.SetParent(toggleHolderRight);

                newElement.toggle.transform.localRotation = Quaternion.identity;
                newElement.toggle.transform.localPosition = Vector3.zero;
                addLeft = !addLeft;

                toggles.Add(newElement);
            }
        }

        void SubmitButtonPressed()
        {
            ValidateForm();
            if (valid)
            {
                Contact contact = new Contact();
                contact.EmailAddress = emailAddressInputField.text;
                contact.Name = nameInputField.text;
                contact.JobTitle = jobTitleInputField.text;

                //not sure what happens here. Please ask Katie for explanation. Might be worth checking out the original app.
                List<Contact.RequestedProduct> productList = new List<Contact.RequestedProduct>();
                foreach (ToggleElement toggleElement in toggles)
                {
                    Contact.RequestedProduct product = new Contact.RequestedProduct();
                    product.Name = toggleElement.name;
                    product.IsRequested = toggleElement.toggle.isOn;
                    productList.Add(product);

                }
                contact.RequestedProducts = productList;

                XmlSerializer s = new XmlSerializer(typeof(Contact));

                string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string directoryPath = System.IO.Path.Combine(docPath, "Honeywell Data Capture");

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                string filename = DateTime.UtcNow.ToString("yyyy-MM-dd HHmmss") + ".xml";

                XmlTextWriter writer = new XmlTextWriter(System.IO.Path.Combine(directoryPath, filename), System.Text.Encoding.UTF8);

                s.Serialize(writer, contact);

                writer.Close();

                submitButton.gameObject.SetActive(false);
                completedMessage.gameObject.SetActive(true);
            }
        }

        void CloseInfoForm()
        {
            //Close this info form (delete it...)
            Destroy(gameObject);
        }

        void ValidateForm()
        {
            valid = true;
            if (emailAddressInputField.text == "")
            {
                emailAddressExclamationText.enabled = true;
                valid = false;
            }
            else
            {
                emailAddressExclamationText.enabled = false;
            }
        }
    }

    public class ToggleElement
    { 
        public Toggle toggle;
        public int id;
        public string name;

        public ToggleElement(Toggle _toggleRef, int _id, string _name)
        {
            toggle = _toggleRef;
            id = _id;
            name = _name;
        }
    } 
}

