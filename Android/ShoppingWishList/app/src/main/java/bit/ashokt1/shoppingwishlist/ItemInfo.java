package bit.ashokt1.shoppingwishlist;

import android.app.Activity;
import android.app.FragmentManager;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Matrix;
import android.media.ExifInterface;
import android.os.Bundle;
import android.text.TextUtils;
import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;
import java.io.File;
import java.io.IOException;

public class ItemInfo extends Activity {

    private String itemName;
    private ShoppingListDF confirmChoice;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_item_info);

        ImageView backButton = (ImageView)findViewById(R.id.imageViewWishList2);
        backButton.setOnClickListener(new BackButton());

        ImageView deleteButton = (ImageView)findViewById(R.id.imageViewDelete);
        deleteButton.setOnClickListener(new DeleteButton());

        // Gets the intent from listedItems
        Intent i = getIntent();

        // gets the file name of the picture that was passed through
        String pictureFileName = i.getStringExtra("Picture file name");

        // finds the absolute filename of the picture
        File imgFile = new File(pictureFileName);

        //saves all the details into separate strings
        itemName = i.getStringExtra("Item Name");
        String itemPrice = i.getStringExtra("Item Price");
        String itemStore = i.getStringExtra("item Store");
        String itemDate = i.getStringExtra("Date Item Saved");

        //text fields
        TextView title = (TextView)findViewById(R.id.textViewTitle);
        TextView price = (TextView)findViewById(R.id.textViewPrice);
        TextView store = (TextView)findViewById(R.id.textViewStore);
        TextView date = (TextView)findViewById(R.id.textViewDate);

        //set the text view
        title.setText(itemName);
        price.setText(itemPrice);
        store.setText(itemStore);
        date.setText(itemDate);

        int rotate = getRotation(pictureFileName); //calls the getRotation method and passes in the name of the photo file path

        Matrix matrix = new Matrix();
        matrix.postRotate(rotate);

        // gets the path of the image and makes it readable by the bitmap
        Bitmap myBitmap = BitmapFactory.decodeFile(imgFile.getAbsolutePath());
        Bitmap rotatedBitmap = Bitmap.createBitmap(myBitmap , 0, 0, myBitmap.getWidth(), myBitmap.getHeight(), matrix, true);

        // finds the image view
        ImageView myImage = (ImageView) findViewById(R.id.imageViewItemPhoto);

        // puts the image from the bitmap into the image view
        myImage.setImageBitmap(rotatedBitmap);

    }

    public class BackButton implements View.OnClickListener
    {
        @Override
        public void onClick(View view)
        {
            Intent changeToAllItemsActivity = new Intent(ItemInfo.this, AllItems.class);
            startActivity(changeToAllItemsActivity);
        }
    }

    //give me data method
    public void giveMeMyDate(boolean confirmDetails)
    {
        confirmChoice.dismiss();

        if(confirmDetails)
        {
            String query = "DELETE FROM tblPictures WHERE itemName = '"+itemName+"'";

            Intent changeIntent = new Intent(ItemInfo.this, AllItems.class);
            changeIntent.putExtra("Delete Query", query);
            startActivity(changeIntent);
        }
    }

    public class DeleteButton implements View.OnClickListener
    {
        @Override
        public void onClick(View view)
        {
            confirmChoice = new ShoppingListDF();
            FragmentManager fm = getFragmentManager();
            confirmChoice.show(fm,"confirm");
        }
    }

    public int getRotation(String imgPath) {
        int rotate = 0;

        try
        {
            ExifInterface exIf = new ExifInterface(imgPath);
            String rotationAmount = exIf.getAttribute(ExifInterface.TAG_ORIENTATION);

            if (!TextUtils.isEmpty(rotationAmount))
            {
                int rotationParameter = Integer.parseInt(rotationAmount); //rotation amount gets converted to an integer
                switch(rotationParameter)
                {
                    case ExifInterface.ORIENTATION_NORMAL:
                        rotate=0;
                        break;

                    case ExifInterface.ORIENTATION_ROTATE_90:
                        rotate=90;
                        break;

                    case ExifInterface.ORIENTATION_ROTATE_180:
                        rotate=180;
                        break;

                    case ExifInterface.ORIENTATION_ROTATE_270:
                        rotate=270;
                        break;
                }
            }
        }
        catch (IOException e)
        {
            e.printStackTrace();
        }
        return rotate;
    }
}
