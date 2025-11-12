using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class APIManager : MonoBehaviour
{
    // Coloque aqui o link da sua API local
    string baseUrl = "https://localhost:5160/api/player/";

    // Para ignorar o certificado SSL local (só para testes)
    class BypassCertificate : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            return true;
        }
    }

    // GET - Pegar todos os players
    public IEnumerator GetPlayers()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(baseUrl + "GetPlayers"))
        {
            www.certificateHandler = new BypassCertificate();
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Players recebidos: " + www.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Erro ao buscar players: " + www.error);
            }
        }
    }

    // GET - Pegar player específico
    public IEnumerator GetPlayer(int id)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(baseUrl + "GetPlayer/" + id))
        {
            www.certificateHandler = new BypassCertificate();
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Player recebido: " + www.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Erro ao buscar player: " + www.error);
            }
        }
    }

    // POST - Adicionar player
    public IEnumerator AddPlayer(Player newPlayer)
    {
        string jsonData = JsonUtility.ToJson(newPlayer);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);

        using (UnityWebRequest www = new UnityWebRequest(baseUrl + "AddPlayer", "POST"))
        {
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.certificateHandler = new BypassCertificate();

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Player adicionado com sucesso!");
            }
            else
            {
                Debug.LogError("Erro ao adicionar player: " + www.error);
            }
        }
    }

    // PUT - Atualizar player existente
    public IEnumerator UpdatePlayer(int id, Player updatedPlayer)
    {
        string jsonData = JsonUtility.ToJson(updatedPlayer);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);

        using (UnityWebRequest www = new UnityWebRequest(baseUrl + "UpdatePlayer/" + id, "PUT"))
        {
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.certificateHandler = new BypassCertificate();

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Player atualizado!");
            }
            else
            {
                Debug.LogError("Erro ao atualizar player: " + www.error);
            }
        }
    }

    // DELETE - Deletar player
    public IEnumerator DeletePlayer(int id)
    {
        using (UnityWebRequest www = UnityWebRequest.Delete(baseUrl + "DeletePlayer/" + id))
        {
            www.certificateHandler = new BypassCertificate();
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Player deletado!");
            }
            else
            {
                Debug.LogError("Erro ao deletar player: " + www.error);
            }
        }
    }
}
