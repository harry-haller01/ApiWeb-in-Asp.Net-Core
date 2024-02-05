using Labiofam.Models;

namespace Labiofam.Services;

/// <summary>
/// Interfaz para el servicio de lectura de archivos en formato json.
/// </summary>
public interface IJsonService
{
    /// <summary>
    /// Lee el archivo provincias-municipios-cuba.json.
    /// </summary>
    /// <returns>Colección de provincias con sus respectivos municipios.</returns>
    ICollection<Province> JsonReader();
}