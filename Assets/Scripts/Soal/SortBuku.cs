using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SortBuku : MonoBehaviour
{
    [SerializeField] private List<Dropable> _column1;
    [SerializeField] private List<Dropable> _column2;
    [SerializeField] private List<Dropable> _column3;
    
    [SerializeField] private List<string> _answersColumn1;
    [SerializeField] private List<string> _answersColumn2;
    [SerializeField] private List<string> _answersColumn3;

    [SerializeField] private GameObject _benarPanel;
    [SerializeField] private GameObject _salahPanel;
    [SerializeField] private GameObject _kosongPanel;
    [SerializeField] private GameObject _belumLengkapPanel;

    public int nullCount;
    public void OnSubmit()
    {
        AudioManager.Instance.PlaySFX("Button");
        CheckAnswers();
    }

    private void CheckAnswers()
    {
        _answersColumn1.Clear();
        _answersColumn2.Clear();
        _answersColumn3.Clear();

        nullCount = _column1.Count + _column2.Count + _column3.Count;
        
        foreach (var _slot in _column1)
        {
            try
            {
                var answer = _slot.transform.GetChild(0);
                _answersColumn1.Add(answer.name);
            }
            catch (Exception)
            {
                _answersColumn1.Add("null");
                nullCount--;
            }
        }
        
        foreach (var _slot in _column2)
        {
            try
            {
                var answer = _slot.transform.GetChild(0);
                _answersColumn2.Add(answer.name);
            }
            catch (Exception)
            {
                _answersColumn2.Add("null");
                nullCount--;
            }
        }
        
        foreach (var _slot in _column3)
        {
            try
            {
                var answer = _slot.transform.GetChild(0);
                _answersColumn3.Add(answer.name);
            }
            catch (Exception)
            {
                _answersColumn3.Add("null");
                nullCount--;
            }
        }

        if (nullCount == 0)
        {
            _kosongPanel.SetActive(true);
            AudioManager.Instance.PlaySFX("Wrong");
            return;
        }

        if (nullCount > 0 && nullCount < _column1.Count + _column2.Count + _column3.Count)
        {
            _belumLengkapPanel.SetActive(true);
            AudioManager.Instance.PlaySFX("Wrong");
            return;
        }

        var rightCount = 0;

        if (_answersColumn1.All(x => x.Substring(0,x.Length-2) == _answersColumn1[0].Substring(0,x.Length-2) && x != "null"))
        {
            var lastIdx = 0;
            foreach (var answer in _answersColumn1)
            {
                var newIdx = int.Parse(answer.Substring(answer.Length-1));
                if (newIdx >= lastIdx)
                {
                    lastIdx = newIdx;
                    rightCount++;
                }
            }
        }
        if (_answersColumn2.All(x => x.Substring(0,x.Length-2) == _answersColumn2[0].Substring(0,x.Length-2) && x != "null"))
        {
            var lastIdx = 0;
            foreach (var answer in _answersColumn2)
            {
                var newIdx = int.Parse(answer.Substring(answer.Length-1));
                if (newIdx >= lastIdx)
                {
                    lastIdx = newIdx;
                    rightCount++;
                }
            }
        }
        if (_answersColumn3.All(x => x.Substring(0,x.Length-2) == _answersColumn3[0].Substring(0,x.Length-2) && x != "null"))
        {
            var lastIdx = 0;
            foreach (var answer in _answersColumn3)
            {
                var newIdx = int.Parse(answer.Substring(answer.Length-1));
                if (newIdx >= lastIdx)
                {
                    lastIdx = newIdx;
                    rightCount++;
                }
            }
        }
        
        if (rightCount < 11)
        {
            _salahPanel.SetActive(true);
            AudioManager.Instance.PlaySFX("Wrong");
        }
        else
        {
            _benarPanel.SetActive(true);
            AudioManager.Instance.PlaySFX("Correct");
        }
    }
    public void OnSkip()
{
    // Hapus semua isi kolom
    ClearColumns();

    // Cari semua GameObject dengan tag "Answer"
    GameObject[] allAnswers = GameObject.FindGameObjectsWithTag("Answer");

    // Mengelompokkan jawaban berdasarkan kategori
    List<GameObject> fantasyAnswers = allAnswers.Where(obj => obj.name.Contains("Petualangan")).ToList();
    List<GameObject> mysteryAnswers = allAnswers.Where(obj => obj.name.Contains("Misteri")).ToList();
    List<GameObject> educationAnswers = allAnswers.Where(obj => obj.name.Contains("Pendidikan")).ToList();

    // Urutkan objek berdasarkan angka di nama mereka
    fantasyAnswers = SortAnswersByNumber(fantasyAnswers);
    mysteryAnswers = SortAnswersByNumber(mysteryAnswers);
    educationAnswers = SortAnswersByNumber(educationAnswers);

    // Isi _column1 dengan jawaban yang benar (Fantasy)
    FillColumnWithAnswers(_column1, fantasyAnswers);

    // Isi _column2 dengan jawaban yang benar (Mystery)
    FillColumnWithAnswers(_column2, mysteryAnswers);

    // Isi _column3 dengan jawaban yang benar (Education)
    FillColumnWithAnswers(_column3, educationAnswers);
}

private void ClearColumns()
{
    // Bersihkan semua slot di kolom
    foreach (var slot in _column1)
    {
        if (slot.transform.childCount > 0)
        {
            Destroy(slot.transform.GetChild(0).gameObject);
        }
    }

    foreach (var slot in _column2)
    {
        if (slot.transform.childCount > 0)
        {
            Destroy(slot.transform.GetChild(0).gameObject);
        }
    }

    foreach (var slot in _column3)
    {
        if (slot.transform.childCount > 0)
        {
            Destroy(slot.transform.GetChild(0).gameObject);
        }
    }
}

private void FillColumnWithAnswers(List<Dropable> column, List<GameObject> answers)
{
    for (int i = 0; i < column.Count; i++)
    {
        // Pastikan ada cukup jawaban untuk mengisi kolom
        if (i < answers.Count)
        {
            // Pindahkan objek ke kolom yang sesuai
            GameObject answer = answers[i];
            answer.transform.SetParent(column[i].transform);  // Set parent ke slot kolom
            answer.transform.localPosition = Vector3.zero;   // Reset posisi agar pas di slot
        }
    }
}

// Fungsi untuk mengurutkan jawaban berdasarkan angka yang ada di nama objek
private List<GameObject> SortAnswersByNumber(List<GameObject> answers)
{
    return answers.OrderBy(obj =>
    {
        // Mengambil angka dari nama objek, misalnya Fantasy1 -> angka 1
        var numberPart = new string(obj.name.Where(char.IsDigit).ToArray());
        return int.Parse(numberPart);
    }).ToList();
}


}
