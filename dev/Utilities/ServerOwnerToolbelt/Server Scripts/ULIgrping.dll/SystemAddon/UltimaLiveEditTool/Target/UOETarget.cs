using System;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;
using Server.Commands;
using Server.Targeting;
using System.Collections;
using System.Collections.Generic;

namespace Server.Targets
{
    	public class UOETarget : Target
	{	        
		private Item tool_i;

		public Item i_Tool
		{ 
			get{ return tool_i; } 
			set{ tool_i = value; } 
		}
    
		public UOETarget() : base( -1, true, TargetFlags.None )
            	{
            	}

		public void SYSGump( Mobile m, Item i )
		{
			PlayerMobile pm = m as PlayerMobile;

			UOETool dd = i as UOETool;

			if (pm == null || dd == null)
    				return;

			dd.SendSYSBCK( pm, dd );
		}

		protected override void OnTarget( Mobile m, object targeted )
		{
			PlayerMobile pm = m as PlayerMobile;

			if (pm == null || pm.Backpack == null)
    				return;
			
			Item check = pm.Backpack.FindItemByType(typeof(UOETool) );

			if ( check == null )
			{
				pm.SendMessage( pm.Name + ", Contact Draco, System Error : Check Failed {0}", check );

				return;
			}

			UOETool dd = check as UOETool;

			if ( targeted is LandTarget )
			{
				if ( dd.LndT == true )
				{
					dd.TempN = ( (LandTarget)targeted ).Name;
					dd.TempID = ( (LandTarget)targeted ).TileID;
					dd.TempX = ( (LandTarget)targeted ).X;
					dd.TempY = ( (LandTarget)targeted ).Y;
					dd.TempZ = ( (LandTarget)targeted ).Z;

					SYSGump( pm, dd );
					return;
				}
				else
					SYSGump( pm, dd );
				return;
			}

			if ( targeted is StaticTarget )
			{
				if ( dd.StcT == true )
				{
					dd.TempN = ( (StaticTarget)targeted ).Name;
					dd.TempID = ( (StaticTarget)targeted ).ItemID;
					dd.TempX = ( (StaticTarget)targeted ).X;
					dd.TempY = ( (StaticTarget)targeted ).Y;
					dd.TempZ = ( (StaticTarget)targeted ).Z;
					//dd.TempH = ( (StaticTarget)targeted ).Hue;

					SYSGump( pm, dd );
					return;
				}
				else
					SYSGump( pm, dd );
				return;
			}
			
			pm.SendMessage( pm.Name + ", Thats not a land or static tile!");

			SYSGump( pm, dd );

			return;
		}		
    	}
}