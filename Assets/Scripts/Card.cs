
using UnityEngine;
using System.Collections;


public enum Alphabet{ HIRAGANA, KATAKANA, ROMAJI };


public enum Character{
	A, I, U, E, O,
	KA, KI, KU, KE, KO,
	SA, SHI, SU, SE, SO,
	TA, CHI, TSU, TE, TO,
	NA, NI, NU, NE, NO,
	HA, HI, FU, HE, HO,
	MA, MI, MU, ME, MO,
	YA, YU, YO,
	RA, RI, RU, RE, RO,
	WA, WO,
	GA, GI, GU, GE, GO,
	ZA, JI, ZU, ZE, ZO,
	DA, DJI, DZU, DE, DO,
	BA, BI, BU, BE, BO,
	PA, PI, PU, PE, PO,

	KYA, KYU, KYO,
	SHA, SHU, SHO,
	CHA, CHU, CHO,
	NYA, NYU, NYO,
	HAY, HYU, HYO,
	MYA, MUY, MYO,
	RYA, RYU, RYO,
	GYA, GYU, GYO,
	JA, JU, JO,
	BYA, BYU, BYO,
	PYA, PYU, PYO,
	N,
	VA, VI, VU, VE, VO,
	DU,
	TI,
	FA, FI, FE, FO,
	SHE,
	JE,
	CHE
 };

public class Card : MonoBehaviour {

	public string cardName;
	public Alphabet alphabet;
	public Character character;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void Flip () {

		//transform.rotation.z += 180;

		print ("ROTANDO...");
		transform.Rotate( new Vector3(0, 0, 180) );

	}
}
