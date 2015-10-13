/*
 * Created by SharpDevelop.
 * User: Minso
 * Date: 30/05/2013
 * Time: 17:15
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;


namespace Import_Export_Universel
{
	/// <summary>
	/// Description of ItemDeListe.
	/// </summary>
	public class ItemDeListe
	{
		private object objetSource;
		private string texte;
		
		public object ObjetSource 
		{
			get {	return this.objetSource; 	}
			set	{	this.objetSource = value;	}
		}
		public string Texte 
		{
			get {	return this.texte; 			}
			set	{	this.objetSource = value;	}
		}
		
		public ItemDeListe()
		{
			this.objetSource = null;
			this.texte = null;			
		}
		
		public ItemDeListe( object _source, string _texte)
		{
			this.objetSource = _source;
			this.texte = _texte;
		}
		
		#region Equals implementation
		public override bool Equals(object obj)
		{
			ItemDeListe other = obj as ItemDeListe;
			
			if (other != null)
			{
				if (ReferenceEquals(this, other))
					return true;

				return this.objetSource.Equals( other.objetSource );
			}
			
			return false;
		}
		
		public override int GetHashCode()
		{
//			int hashCode = 0;
//			unchecked {
//				if (objetSource != null)
//					hashCode += 1000000007 * objetSource.GetHashCode();
//				if (texte != null)
//					hashCode += 1000000009 * texte.GetHashCode();
//			}
			return this.objetSource.GetHashCode();
		}

		
		public static bool operator ==(ItemDeListe lhs, ItemDeListe rhs)
		{
			if (ReferenceEquals(lhs, rhs))
				return true;
			
			if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
				return false;
			
			return lhs.Equals(rhs);
		}
		
		public static bool operator !=(ItemDeListe lhs, ItemDeListe rhs)
		{
			return !(lhs == rhs);
		}

		public override string ToString()
		{
			return texte;
		}
		#endregion

	}
}
