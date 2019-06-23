using UnityEngine;

public class FeatureLinkConnector : MonoBehaviour
{
    public FeatureLink link;
    private void Start()
    {
        link.feature = GetComponent<FeatureController>();
    }
}
