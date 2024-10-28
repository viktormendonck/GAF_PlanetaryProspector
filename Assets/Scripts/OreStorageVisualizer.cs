
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class OreStorageVisualizer : MonoBehaviour
{
    [SerializeField] private OreContainer oreContainer;
    [SerializeField] private Image fillImage;
    // Update is called once per frame
    void Update()
    {
        fillImage.fillAmount = oreContainer.GetCurrentOreAmount() / oreContainer.GetMaxOreAmount();
    }
}
