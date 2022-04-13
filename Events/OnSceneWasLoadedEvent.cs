namespace Trinity.Events
{
    public interface OnSceneLoadedEvent
    {
        void OnSceneWasLoadedEvent(int buildIndex, string sceneName);
    }
}
