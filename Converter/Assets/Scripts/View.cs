using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class View : MonoBehaviour
{
    [Inject] protected ViewManager _viewManager;
    public virtual void Initialize()
    {
    }
    public virtual void Show() => gameObject.SetActive(true);
    public virtual void Hide() => gameObject.SetActive(false);
}
