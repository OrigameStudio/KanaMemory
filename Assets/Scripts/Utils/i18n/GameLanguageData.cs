
using UnityEngine;
using System.Collections;


[System.Serializable]
public class GameLanguageData{

	public string		name			= "English";
	public Texture2D	texture;

	public string		loading			= "Loading...";

	/* home */
	public string		play			= "PLAY";
	public string		dojo			= "STUDY";

	/* dojo */
	public string		welcome			= "WELCOME TO\nTHE DOJO";
	public string		studyHiragana	= "STUDY\nHIRAGANA";
	public string		studyKatakana	= "STUDY\nKATAKANA";
	public string		goodbye			= "EXIT\nDOJO?";
	public string		exit			= "YES";
	public string		stay			= "NO";

	/* start */
	public string		newGame			= "New Game";
	public string		difficulty		= "DIFFICULTY";
	public string		easy			= "EASY";
	public string		medium			= "MEDIUM";
	public string		hard			= "HARD";
	public string		boardSize		= "BOARD SIZE";
	public string		small			= "SMALL";
	public string		regular			= "REGULAR";
	public string		big				= "BIG";
	public string		gameType		= "GAME TYPE";

	/* game */
	public string		confirmQuitGame	= "QUIT?";
	public string		quitGame		= "YES";
	public string		keepPlaying		= "NO";

	/* ok */
	public GameObject	Mesh_OK;

	/* ko */
	public GameObject	Mesh_KO;

	/* about */
	public string		rateThisApp		= "Rate this app";

	/* admob keywords */
	public string[] keywords = {

		"Japanese",
		"Hiragana",
		"Katakana",
		"Kana",
		"Kanji",
		"Learn",
		"Study",
		"Language",
		"Game",

		"Travel",
		"Japan",
		"JP",
		"Asia",
		"Honshu",
		"Hokkaido",
		"Kyushu",
		"Shikoku",
		"Tokyo",
		"Yokohama",
		"Osaka",
		"Nagoya",
		"Kobe",
		"Kyoto",
		"Fukuoka",
		"Kawasaki",
		"Saitama",
		"Hiroshima",
		"Miyajima",
		"Okinawa",
		"Kiyomizudera",
		"Ginkaku",
		"Jinkaku",
		"Shrine",
		"Zen",
		"Shibuya",
		"Harajuku",
		"Asakusa",
		"Ueno",
		"Akihabara",
		"Akiba",

		"Chopsticks",
		"Sushi",
		"Wasabi",
		"Tempura",
		"Ramen",
		"Onigiri",
		"Teriyaki",
		"Yakitori",
		"Okonomiyaki",
		"Gyoza",
		"Yakisoba",
		"Sukiyaki",
		"Bento",
		"Sake",
		"Asahi",
		"Kirin",
		"Sapporo",

		"Ikebana",
		"Bonsai",
		"Taiko",
		"Kabuki",
		"Origami",
		"Sumo",
		"Kendo",
		"Judo",
		"Aikido",
		"Kyudo",
		"Karate",
		"Samurai",
		"Shogun",
		"Katana",
		"Ninja",
		"Akira",
		"Doraemon",
		"Ranma",
		"Naruto",
		"Pokemon",
		"Shinchan",
		"Layton",
		"Astroboy",
		"Godzilla",
		"Gamera",
		"Dragon",
		"Tanuki",
		"Kappa",
		"Daruma",

		"Manga",
		"Anime",
		"Otaku",
		"Nintendo",
		"Ghibli",
		"Tototro",
		"Ponyo",
		"Chihiro",
		"Miyazaki",
		"Idol",
		"Pachinko",
		"Sakura",
		"Hanami",
		"Hachiko",
		"Karaoke",
		"J-POP",
		"AKB48",
		"Dorama",
		"Murakami",
		"Toriyama",
		"Kotoba",
		"Jitensha",
		"Kuma",
		"Neko",
		"Tanaka",
		"Taro",
		"Kawai",
		"Sugoi",
		"Banzai",
		"Kanpai",
		"Yamanote",
		"Shinkansen",
		"JR",
		"NHK",
		"Nihon",
		"Nippon",
		"Nihongo",
	};
}
