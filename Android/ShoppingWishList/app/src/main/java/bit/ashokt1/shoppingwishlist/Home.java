package bit.ashokt1.shoppingwishlist;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.ImageView;


public class Home extends Activity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_home);

        ImageView wishListIcon = (ImageView)findViewById(R.id.imageViewWishlist);
        wishListIcon.setOnClickListener(new WishListButton());

        ImageView informationIcon = (ImageView)findViewById(R.id.imageViewInformation);
        informationIcon.setOnClickListener(new InformationButton());
    }

    public class WishListButton implements View.OnClickListener
    {
        @Override
        public void onClick(View view)
        {
            Intent changeToAllItemsActivity = new Intent(Home.this, AllItems.class);
            startActivity(changeToAllItemsActivity);
        }
    }

    public class InformationButton implements View.OnClickListener
    {
        @Override
        public void onClick(View view)
        {
            Intent changeToAboutActivity = new Intent(Home.this, About.class);
            startActivity(changeToAboutActivity);
        }
    }
}
