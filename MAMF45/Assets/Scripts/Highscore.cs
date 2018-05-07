using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEngine.UI;

public class Highscore : MonoBehaviour {
	private class HighscoreEntry {
		public int Score;

		public HighscoreEntry (int score)
		{
			Score = score;
		}

		public override string ToString ()
		{
			return string.Format ("%d", Score);
		}
	}
	public string highscoreFilename;
	public Text HighscoreBoard;

	public void SaveHighscores() {

	}

	public void LoadHighscores() {
		try {
			var hsFile = File.Open(highscoreFilename, FileMode.Open);

			var fileBuffer = new byte[hsFile.Length];
			hsFile.Read(fileBuffer, 0, (int) hsFile.Length);
			var fileString = Encoding.Unicode.GetString(fileBuffer);

			DisplayHighscores(ParseLines(fileString));
		}
		catch (IOException e) {
			Debug.LogException (e);
		}
	}

	private IEnumerable<HighscoreEntry> ParseLines(string lines) {
		var split = lines.Split (new[] {"\n", "\r", "\r\n"}, System.StringSplitOptions.RemoveEmptyEntries);

		foreach (var line in split) {
			yield return new HighscoreEntry (int.Parse (line));
		}
	}

	private void DisplayHighscores(IEnumerable<HighscoreEntry> highscores) {
		var sb = new StringBuilder ();
		sb.AppendLine ("Highscores");

		foreach (var hs in highscores) {
			sb.AppendLine(string.Format("%d", hs.Score));
		}
		HighscoreBoard.text = sb.ToString ();
	}
}
