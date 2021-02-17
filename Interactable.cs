using UnityEngine;
using UnityEngine.UI;

namespace TauntGames
{
    public class Interactable : MonoBehaviour
    {
        #region Class Variables


        public Text interactionText;
        // public TextMeshProUGUI informationText;
        // public int showInfoTime;
        public string actionString;
        // private bool fadeIn;
        // private bool fadeOut;
        public bool canInteract;


        #endregion


        #region Class Methods

        public virtual void OnBeginFocus()
        {

            if (canInteract)
            {
                UI_Control.onPlayerNeedsInfo?.Invoke(actionString);
                // interactionText.text = actionString;
                // interactionText.gameObject.SetActive(true);
            }
        }

        public virtual void CustomUpdate()
        {
            
        }
        public virtual void Interact()
        {
           
        }
        public virtual void OnEndFocus()
        {
            if (canInteract)
            {
     
                UI_Control.onPlayerNeedsInfo?.Invoke("");
                // interactionText.text = "";
                // interactionText.gameObject.SetActive(false);
            }
        }

        #endregion

        #region Helper Methods

        // public void ShowInformation(string _infoText, Color32 _faceColor)
        // {
        //     informationText.color = _faceColor;
        //     informationText.text = "";
        //     informationText.gameObject.SetActive(false);
        //     StopCoroutine(nameof(ShowInformation));
        //     StartCoroutine(InfoText(_infoText));
        // }
        //
        // IEnumerator InfoText(string _text)
        // {
        //     informationText.text = _text;
        //     informationText.gameObject.SetActive(true);
        //     yield return new WaitForSeconds(showInfoTime);
        //     informationText.text = "";
        //     informationText.gameObject.SetActive(false);
        // }

        // public void switchHighlighted(bool highlighted)
        // {
        //     originalMat.SetFloat("_Highlighted", (highlighted ? 1.0f : 0.0f));
        // }

        #endregion
    }
}