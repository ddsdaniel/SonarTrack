namespace SonarTrack.Infrastructure.Extensions
{
    internal static class LinqExtensions
    {
        // Implementação de uma extensão Batch se ela não estiver disponível no seu ambiente
        public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> source, int size)
        {
            while (source.Any())
            {
                yield return source.Take(size);
                source = source.Skip(size);
            }
        }
    }
}
