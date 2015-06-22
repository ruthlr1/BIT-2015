package bit.ashokt1.shoppingwishlist;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.content.res.Resources;
import android.graphics.Color;
import android.graphics.drawable.Drawable;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.TextView;

public class About extends Activity {

    Icons[] iconArray;
    ImageView wishList;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_about);

        initialiseDataArray();

        //create the adapter
        IconArrayAdapter iconAdapter = new IconArrayAdapter(this,
                R.layout.custom_listview_layout, iconArray);

        //bind LV to the adapter
        ListView lvIcons = (ListView)findViewById(R.id.LVIcons);
        lvIcons.setAdapter(iconAdapter);

        wishList = (ImageView)findViewById(R.id.imageViewAllItemss);
        wishList.setOnClickListener(new WishListIntent());
    }

    private void initialiseDataArray()
    {
        //Fetch the drawables

        Resources resourceMachine = getResources();

        Drawable listViewIconImage = resourceMachine.getDrawable(R.drawable.ic_list_black_24dp);
        Drawable infoIconImage = resourceMachine.getDrawable(R.drawable.ic_info_black_24dp);
        Drawable addItemIcon = resourceMachine.getDrawable(R.drawable.ic_playlist_add_black_24dp);
        Drawable saveImageIcon = resourceMachine.getDrawable(R.drawable.ic_save_black_24dp);
        Drawable cameraIconImage = resourceMachine.getDrawable(R.drawable.ic_camera_alt_black_24dp);
        Drawable deleteIconImage = resourceMachine.getDrawable(R.drawable.ic_delete_black_24dp);

        //initialising the icon array
        iconArray = new Icons[6];

        iconArray[0] = new Icons(listViewIconImage, "Wish List", "Takes user to the All Items page" );
        iconArray[1] = new Icons(infoIconImage,"Information", "Takes user to the About page" );
        iconArray[2] = new Icons(addItemIcon,"Add New Item", "Takes user to the New Item page" );
        iconArray[3] = new Icons(saveImageIcon, "Save New Item", "Allows user to save a new item" );
        iconArray[4] = new Icons(cameraIconImage, "Camera", "Allows user to take a photo");
        iconArray[5] = new Icons(deleteIconImage, "Delete Item", "Delete an item from the wish list");

    }

    public class IconArrayAdapter extends ArrayAdapter<Icons> {
        public IconArrayAdapter(Context context, int resource, Icons[] objects) {
            super(context, resource, objects);
        }

        @Override
        public View getView(int position, View convertView, ViewGroup container) {
            LayoutInflater inflater = LayoutInflater.from(About.this);

            View customView = inflater.inflate(R.layout.custom_listview_layout, container, false);

            ImageView itemImageView = (ImageView) customView.findViewById(R.id.ivItemImage);
            TextView itemTextView = (TextView) customView.findViewById(R.id.tvItemWords);

            Icons currentItem = getItem(position);
            itemTextView.setTextColor(Color.BLACK);
            itemImageView.setImageDrawable(currentItem.iconImage);
            itemTextView.setText(currentItem.toString());

            return customView;

        }
    }


    public class WishListIntent implements View.OnClickListener
    {
        @Override
        public void onClick(View view)
        {
            Intent changeToAllItemsIntent = new Intent(About.this, AllItems.class);
            startActivity(changeToAllItemsIntent);
        }
    }
}
