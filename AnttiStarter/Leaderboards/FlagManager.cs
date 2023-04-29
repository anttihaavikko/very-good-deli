﻿using Godot;

namespace Leaderboards
{
	public class FlagManager
	{
		public static Vector2 Get(string country) {
			if(country == "bj") return new Vector2(256,32);
			if(country == "tm") return new Vector2(160,416);
			if(country == "eg") return new Vector2(384,96);
			if(country == "tv") return new Vector2(320,416);
			if(country == "kz") return new Vector2(320,224);
			if(country == "lk") return new Vector2(0,256);
			if(country == "ai") return new Vector2(128,0);
			if(country == "dj") return new Vector2(160,96);
			if(country == "va") return new Vector2(96,448);
			if(country == "gy") return new Vector2(288,160);
			if(country == "fi") return new Vector2(64,128);
			if(country == "ne") return new Vector2(0,320);
			if(country == "jp") return new Vector2(448,192);
			if(country == "cv") return new Vector2(32,96);
			if(country == "nz") return new Vector2(224,320);
			if(country == "pa") return new Vector2(288,320);
			if(country == "mn") return new Vector2(32,288);
			if(country == "dm") return new Vector2(224,96);
			if(country == "bh") return new Vector2(192,32);
			if(country == "de") return new Vector2(128,96);
			if(country == "ki") return new Vector2(96,224);
			if(country == "nl") return new Vector2(96,320);
			if(country == "ye") return new Vector2(352,448);
			if(country == "it") return new Vector2(288,192);
			if(country == "lb") return new Vector2(384,224);
			if(country == "ru") return new Vector2(320,352);
			if(country == "zm") return new Vector2(416,448);
			if(country == "mm") return new Vector2(0,288);
			if(country == "in") return new Vector2(160,192);
			if(country == "fo") return new Vector2(160,128);
			if(country == "cd") return new Vector2(128,64);
			if(country == "ve") return new Vector2(160,448);
			if(country == "qa") return new Vector2(192,352);
			if(country == "gi") return new Vector2(448,128);
			if(country == "mr") return new Vector2(128,288);
			if(country == "gg") return new Vector2(384,128);
			if(country == "do") return new Vector2(256,96);
			if(country == "sk") return new Vector2(128,384);
			if(country == "bz") return new Vector2(64,64);
			if(country == "se") return new Vector2(32,384);
			if(country == "uz") return new Vector2(64,448);
			if(country == "rw") return new Vector2(352,352);
			if(country == "tt") return new Vector2(288,416);
			if(country == "ma") return new Vector2(224,256);
			if(country == "mh") return new Vector2(384,256);
			if(country == "at") return new Vector2(352,0);
			if(country == "om") return new Vector2(256,320);
			if(country == "fj") return new Vector2(96,128);
			if(country == "bs") return new Vector2(416,32);
			if(country == "pf") return new Vector2(352,320);
			if(country == "gm") return new Vector2(32,160);
			if(country == "sz") return new Vector2(416,384);
			if(country == "ga") return new Vector2(224,128);
			if(country == "lu") return new Vector2(128,256);
			if(country == "np") return new Vector2(160,320);
			if(country == "bg") return new Vector2(160,32);
			if(country == "pr") return new Vector2(32,352);
			if(country == "be") return new Vector2(96,32);
			if(country == "sg") return new Vector2(64,384);
			if(country == "ls") return new Vector2(64,256);
			if(country == "pt") return new Vector2(96,352);
			if(country == "am") return new Vector2(192,0);
			if(country == "to") return new Vector2(224,416);
			if(country == "je") return new Vector2(352,192);
			if(country == "ee") return new Vector2(352,96);
			if(country == "nc") return new Vector2(448,288);
			if(country == "vn") return new Vector2(256,448);
			if(country == "gw") return new Vector2(256,160);
			if(country == "hu") return new Vector2(448,160);
			if(country == "md") return new Vector2(288,256);
			if(country == "ci") return new Vector2(256,64);
			if(country == "jm") return new Vector2(384,192);
			if(country == "gr") return new Vector2(160,160);
			if(country == "ml") return new Vector2(448,256);
			if(country == "km") return new Vector2(128,224);
			if(country == "vi") return new Vector2(224,448);
			if(country == "ps") return new Vector2(64,352);
			if(country == "bo") return new Vector2(352,32);
			if(country == "zw") return new Vector2(448,448);
			if(country == "sn") return new Vector2(224,384);
			if(country == "mx") return new Vector2(320,288);
			if(country == "uy") return new Vector2(32,448);
			if(country == "vu") return new Vector2(288,448);
			if(country == "ni") return new Vector2(64,320);
			if(country == "ch") return new Vector2(224,64);
			if(country == "ag") return new Vector2(96,0);
			if(country == "sd") return new Vector2(0,384);
			if(country == "bw") return new Vector2(0,64);
			if(country == "us") return new Vector2(0,448);
			if(country == "ph") return new Vector2(416,320);
			if(country == "lc") return new Vector2(416,224);
			if(country == "is") return new Vector2(256,192);
			if(country == "ws") return new Vector2(320,448);
			if(country == "ly") return new Vector2(192,256);
			if(country == "gn") return new Vector2(64,160);
			if(country == "gu") return new Vector2(224,160);
			if(country == "ke") return new Vector2(0,224);
			if(country == "hr") return new Vector2(384,160);
			if(country == "ge") return new Vector2(320,128);
			if(country == "hk") return new Vector2(320,160);
			if(country == "re") return new Vector2(224,352);
			if(country == "al") return new Vector2(160,0);
			if(country == "co") return new Vector2(416,64);
			if(country == "er") return new Vector2(448,96);
			if(country == "sy") return new Vector2(384,384);
			if(country == "ro") return new Vector2(256,352);
			if(country == "th") return new Vector2(64,416);
			if(country == "sl") return new Vector2(160,384);
			if(country == "nr") return new Vector2(192,320);
			if(country == "br") return new Vector2(384,32);
			if(country == "ec") return new Vector2(320,96);
			if(country == "kr") return new Vector2(224,224);
			if(country == "pg") return new Vector2(384,320);
			if(country == "mz") return new Vector2(384,288);
			if(country == "sa") return new Vector2(384,352);
			if(country == "gt") return new Vector2(192,160);
			if(country == "kg") return new Vector2(32,224);
			if(country == "bi") return new Vector2(224,32);
			if(country == "cu") return new Vector2(0,96);
			if(country == "tg") return new Vector2(32,416);
			if(country == "dz") return new Vector2(288,96);
			if(country == "bt") return new Vector2(448,32);
			if(country == "lr") return new Vector2(32,256);
			if(country == "rs") return new Vector2(288,352);
			if(country == "tw") return new Vector2(352,416);
			if(country == "aw") return new Vector2(416,0);
			if(country == "py") return new Vector2(160,352);
			if(country == "kh") return new Vector2(64,224);
			if(country == "gq") return new Vector2(128,160);
			if(country == "gh") return new Vector2(416,128);
			if(country == "st") return new Vector2(320,384);
			if(country == "tn") return new Vector2(192,416);
			if(country == "mv") return new Vector2(256,288);
			if(country == "ky") return new Vector2(288,224);
			if(country == "hn") return new Vector2(352,160);
			if(country == "fr") return new Vector2(192,128);
			if(country == "si") return new Vector2(96,384);
			if(country == "id") return new Vector2(0,192);
			if(country == "au") return new Vector2(384,0);
			if(country == "tr") return new Vector2(256,416);
			if(country == "my") return new Vector2(352,288);
			if(country == "lt") return new Vector2(96,256);
			if(country == "li") return new Vector2(448,224);
			if(country == "cg") return new Vector2(192,64);
			if(country == "by") return new Vector2(32,64);
			if(country == "bn") return new Vector2(320,32);
			if(country == "mg") return new Vector2(352,256);
			if(country == "fm") return new Vector2(128,128);
			if(country == "tz") return new Vector2(384,416);
			if(country == "sb") return new Vector2(416,352);
			if(country == "as") return new Vector2(320,0);
			if(country == "il") return new Vector2(64,192);
			if(country == "ng") return new Vector2(32,320);
			if(country == "mk") return new Vector2(416,256);
			if(country == "no") return new Vector2(128,320);
			if(country == "af") return new Vector2(64,0);
			if(country == "ua") return new Vector2(416,416);
			if(country == "sc") return new Vector2(448,352);
			if(country == "cf") return new Vector2(160,64);
			if(country == "ck") return new Vector2(288,64);
			if(country == "cr") return new Vector2(448,64);
			if(country == "kw") return new Vector2(256,224);
			if(country == "ae") return new Vector2(32,0);
			if(country == "iq") return new Vector2(192,192);
			if(country == "vc") return new Vector2(128,448);
			if(country == "gd") return new Vector2(288,128);
			if(country == "pw") return new Vector2(128,352);
			if(country == "me") return new Vector2(320,256);
			if(country == "ad") return new Vector2(0,0);
			if(country == "gb") return new Vector2(256,128);
			if(country == "ug") return new Vector2(448,416);
			if(country == "mo") return new Vector2(64,288);
			if(country == "kp") return new Vector2(192,224);
			if(country == "ba") return new Vector2(0,32);
			if(country == "cn") return new Vector2(384,64);
			if(country == "bf") return new Vector2(128,32);
			if(country == "eh") return new Vector2(416,96);
			if(country == "cy") return new Vector2(64,96);
			if(country == "ir") return new Vector2(224,192);
			if(country == "so") return new Vector2(256,384);
			if(country == "za") return new Vector2(384,448);
			if(country == "tl") return new Vector2(128,416);
			if(country == "es") return new Vector2(0,128);
			if(country == "tc") return new Vector2(448,384);
			if(country == "sm") return new Vector2(192,384);
			if(country == "jo") return new Vector2(416,192);
			if(country == "sr") return new Vector2(288,384);
			if(country == "ht") return new Vector2(416,160);
			if(country == "la") return new Vector2(352,224);
			if(country == "ie") return new Vector2(32,192);
			if(country == "mw") return new Vector2(288,288);
			if(country == "ar") return new Vector2(288,0);
			if(country == "gp") return new Vector2(96,160);
			if(country == "et") return new Vector2(32,128);
			if(country == "mq") return new Vector2(96,288);
			if(country == "mu") return new Vector2(224,288);
			if(country == "bd") return new Vector2(64,32);
			if(country == "bm") return new Vector2(288,32);
			if(country == "pk") return new Vector2(448,320);
			if(country == "gl") return new Vector2(0,160);
			if(country == "pl") return new Vector2(0,352);
			if(country == "cz") return new Vector2(96,96);
			if(country == "dk") return new Vector2(192,96);
			if(country == "mc") return new Vector2(256,256);
			if(country == "td") return new Vector2(0,416);
			if(country == "sv") return new Vector2(352,384);
			if(country == "an") return new Vector2(224,0);
			if(country == "pe") return new Vector2(320,320);
			if(country == "ms") return new Vector2(160,288);
			if(country == "bb") return new Vector2(32,32);
			if(country == "lv") return new Vector2(160,256);
			if(country == "cm") return new Vector2(352,64);
			if(country == "im") return new Vector2(128,192);
			if(country == "vg") return new Vector2(192,448);
			if(country == "az") return new Vector2(448,0);
			if(country == "mt") return new Vector2(192,288);
			if(country == "ca") return new Vector2(96,64);
			if(country == "tj") return new Vector2(96,416);
			if(country == "ao") return new Vector2(256,0);
			if(country == "cl") return new Vector2(320,64);
			if(country == "kn") return new Vector2(160,224);
			if(country == "na") return new Vector2(416,288);
			return Vector2.Zero;
		}
	}
}
