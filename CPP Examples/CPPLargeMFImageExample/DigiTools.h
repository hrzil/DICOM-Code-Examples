/****************************************************************************
**  $Id: DigiTools.h,v 1.2 2012/10/10 13:58:30 zroni Exp $
**
**  DigiTools - Digits to Image
**	Copyright (c) 2012, Roni Zaharia
**  All rights reserved.
**	RZ - Software Services
**	http://www.roniza.com
**
**	This file is part of RZ software libraries.
**
**  DigiTools may be distributed and/or modified under the terms of 
**  the GNU General Public License version 3 as published by the 
**  Free Software Foundation.
**
**  You should have received a copy of the GNU General Public License
**  along with DigiTools. If not, see <http://www.gnu.org/licenses/>.
**
**  DigiTools is distributed in the hope that it will be useful,
**  but WITHOUT ANY WARRANTY; without even the implied warranty of
**  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
**  GNU General Public License for more details.
**
**  See http://www.roniza.com for further details.
**
**  For more information regarding the comercial license please contact
**  RZ Software Services by e-mail info@roniza.com or visit our web
****************************************************************************/

#pragma once

#ifndef _DIGITOOLS_H
#define _DIGITOOLS_H

#define NUM_DATA      10

#define X             5
#define Y             7

#define N             (X * Y)
#define M             10
#define ST 1
#define SB 1
#define SL 1
#define SR 1
#define ND 4
#define IX ((X+SL+SR)*ND)
#define IY (Y+ST+SB)

char Image[IY][IX][3];

char                Pattern[NUM_DATA][Y][X+1] = { { " OOO ",
                                                    "O   O",
                                                    "O   O",
                                                    "O   O",
                                                    "O   O",
                                                    "O   O",
                                                    " OOO "  },

                                                  { "  O  ",
                                                    " OO  ",
                                                    "O O  ",
                                                    "  O  ",
                                                    "  O  ",
                                                    "  O  ",
                                                    "  O  "  },

                                                  { " OOO ",
                                                    "O   O",
                                                    "    O",
                                                    "   O ",
                                                    "  O  ",
                                                    " O   ",
                                                    "OOOOO"  },

                                                  { " OOO ",
                                                    "O   O",
                                                    "    O",
                                                    " OOO ",
                                                    "    O",
                                                    "O   O",
                                                    " OOO "  },

                                                  { "   O ",
                                                    "  OO ",
                                                    " O O ",
                                                    "O  O ",
                                                    "OOOOO",
                                                    "   O ",
                                                    "   O "  },

                                                  { "OOOOO",
                                                    "O    ",
                                                    "O    ",
                                                    "OOOO ",
                                                    "    O",
                                                    "O   O",
                                                    " OOO "  },

                                                  { " OOO ",
                                                    "O   O",
                                                    "O    ",
                                                    "OOOO ",
                                                    "O   O",
                                                    "O   O",
                                                    " OOO "  },

                                                  { "OOOOO",
                                                    "    O",
                                                    "    O",
                                                    "   O ",
                                                    "  O  ",
                                                    " O   ",
                                                    "O    "  },

                                                  { " OOO ",
                                                    "O   O",
                                                    "O   O",
                                                    " OOO ",
                                                    "O   O",
                                                    "O   O",
                                                    " OOO "  },

                                                  { " OOO ",
                                                    "O   O",
                                                    "O   O",
                                                    " OOOO",
                                                    "    O",
                                                    "O   O",
                                                    " OOO "  } };


static int P[ND][4] = {
	{ 255,  255, 255 ,1000 },
	{ 255,  0,   0 ,100  },
	{ 0,    255, 0 ,10   }, 
	{ 0,    0,   255 ,1    }
};

void number2image(int num)
{
	memset(Image, 0, sizeof(Image));
	for (int i=0; i<ND; i++)
	{
		int d = (num/P[i][3])%10;
		for (int y=0; y<Y; y++)
		{
			for (int x=0; x<X; x++)
			{
				if (Pattern[d][y][x] == 'O')
				{
					Image[y+ST][SL+x+(SL+X+SR)*i][0]=P[i][0];
					Image[y+ST][SL+x+(SL+X+SR)*i][1]=P[i][1];
					Image[y+ST][SL+x+(SL+X+SR)*i][2]=P[i][2];
				}
			}
		}
	}
}

void scaleImageTo(int x, int y, char* buf)
{
	int sx = x/(IX);
	int sy = y/(IY);
	for (int iy=0; iy<IY; iy++)
	{
		for (int isy=0; isy<sy; isy++)
		{
			for (int ix=0;ix<IX; ix++)
			{
				for (int isx=0; isx<sx; isx++)
				{
					char* p=buf+((iy*sy+isy)*x+ix*sx+isx)*3;
					p[0] = Image[iy][ix][0];
					p[1] = Image[iy][ix][1];
					p[2] = Image[iy][ix][2];
				}
			}
		}
	}
}

#endif
