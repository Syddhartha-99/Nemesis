using Systems.Collection
using Systems.Collection.Generic;
using UnityEngine;

namespace SG
{
	public class EnemyWeaponSlotManager : MonoBehaviour
	
	WeaponHolderSlot rightHand;
	WeaponHolderSlot leftHand;
	
	DamageCollider leftHandDamagerCollider;
	DamageCollider rightHandDamageCollider;
	
	public void LoadWeaponOnSlot(WeaponItem weapon)
	{
		if (isLeft)
		{
			leftHandSlot.currentWeapon = weapon;
			leftHandSlot.LoadWeaponModel(weapon);
			LoadWeaponsDamageCollider(true);	
		}
		else
		{
			rightHandSlot.currentWeapon = weapon;
			rightHandSlot.LoadWeaponModel(weapon);
			LoadWeaponsDamageCollider(false);
		}
	}
	
	public void LoadWeaponsDamageCollider (bool isLeft)
	{
		if (isLeft)
		{
			leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
		}
		else
		{
						rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
		}
	}
	
	public void OpenDamageCollider()
	{
		rightHandDamageCollider.EnableDamageCollider();
				//change stuff in PlayerManager and WeaponHolderManager
	}
	
	public void CloseDamageCollider()
	{
		rightHandDamageCollider.DisableDamagerCollider();
	}
}