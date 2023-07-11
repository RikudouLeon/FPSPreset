using UnityEditor;

[CustomEditor(typeof(Interaction), true)]
public class InteractionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Interaction interaction = (Interaction)target; // store an instance of the Interaction script. "target" is the currently selected game object that is inspected
        if (target.GetType() == typeof(EventOnlyInteractable))
        {
            interaction.promptMessage = EditorGUILayout.TextField("Prompt Message", interaction.promptMessage);
            EditorGUILayout.HelpBox("EventOnlyInteract can ONLY use UnityEvents.", MessageType.Info);
            if(interaction.GetComponent<InteractEvent>() == null)
            {
                interaction.useEvents = true;
                interaction.gameObject.AddComponent<InteractEvent>();
            }
        }
        else
        {
            base.OnInspectorGUI();
            if (interaction.useEvents)
            {
                // If we are using events, add the component
                if (interaction.GetComponent<InteractEvent>() == null)
                {
                    interaction.gameObject.AddComponent<InteractEvent>();
                }
            }
            // If we are not using events, remove the component
            else
            {
                if (interaction.GetComponent<InteractEvent>() != null)
                {
                    DestroyImmediate(interaction.GetComponent<InteractEvent>());
                }
            }
        }
    }
}
