package bit.ashokt1.shoppingwishlist;

import android.app.AlertDialog;
import android.app.Dialog;
import android.app.DialogFragment;
import android.content.DialogInterface;
import android.os.Bundle;
import android.app.AlertDialog.Builder;

public class ShoppingListDF extends DialogFragment
{
    public ShoppingListDF() {}

    @Override
    public Dialog onCreateDialog(Bundle savedInstanceState)

    {
        Builder builder = new AlertDialog.Builder(getActivity());

        builder.setTitle("Confirm to Delete");
        builder.setPositiveButton("Confirm", new YesButtonHandler());
        builder.setNegativeButton("Cancel", new NoButtonHandler());

        Dialog customDialog = builder.create();

        return customDialog;
    }

    public class YesButtonHandler implements DialogInterface.OnClickListener
    {
        @Override
        public void onClick(DialogInterface dialog, int whichButton)
        {
            ItemInfo mainActivity = (ItemInfo)getActivity();
            mainActivity.giveMeMyDate(true);
        }
    }

    public class NoButtonHandler implements DialogInterface.OnClickListener
    {
        @Override
        public void onClick(DialogInterface dialog, int whichButton)
        {
            ItemInfo mainActivity = (ItemInfo)getActivity();
            mainActivity.giveMeMyDate(false);
        }
    }
}
