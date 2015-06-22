package bit.ashokt1.shoppingwishlist;

import android.app.Activity;
import android.content.Intent;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.graphics.Color;
import android.os.Bundle;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.View;
import android.view.ViewGroup;
import android.view.WindowManager;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;
import java.util.ArrayList;


public class AllItems extends Activity {

    private SQLiteDatabase PictureDB;
    private EditText search;
    private ArrayAdapter<String> ItemListAdapter;
    private TextView feedback;
    private TextView message;
    private ArrayList<String> titles;
    private ImageView about;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_all_items);

        feedback = (TextView)findViewById(R.id.textViewResponse);
        message = (TextView)findViewById(R.id.textViewMessage);

        titles = new ArrayList<String>();

        // Get the Intent
        Intent intentPreviouslyLaunched = getIntent();

        // Create Database
        PictureDB = openOrCreateDatabase("PictureDB",MODE_PRIVATE,null);

        // Creates the table
        String createQuery = "CREATE TABLE IF NOT EXISTS tblPictures(pictureID INTEGER PRIMARY KEY AUTOINCREMENT," +
                " itemName TEXT NOT NULL, picture TEXT NOT NULL, price TEXT NOT NULL, store TEXT NOT NULL, date TEXT NOT NULL);";
        PictureDB.execSQL(createQuery);

        // Gets the Insert string from the MainActivity
        String query = intentPreviouslyLaunched.getStringExtra("Query String");

        // Checks whether the MainActivity has saved any data
        if (query != null)
        {
            PictureDB.execSQL(query);// executes the insert query
        }

        String deleteQuery = intentPreviouslyLaunched.getStringExtra("Delete Query");

        if(deleteQuery!=null)
        {
            PictureDB.execSQL(deleteQuery); //executes the delete query
            Toast.makeText(AllItems.this, "Item deleted!", Toast.LENGTH_LONG).show();
        }
        ArrayList<String> data = GetData();

        if(data.size()==0)
        {
            feedback.setText("No Items");
            message.setText(" ");
        }
        else
        {
            feedback.setText("Your Items");
            message.setText("Click on an item for more information");
            titles = data;
        }

        // finds the correct list view
        ListView ItemList = (ListView) findViewById(R.id.listViewAllItems);
        ItemListAdapter = new ArrayAdapter<String>(this, android.R.layout.simple_list_item_1, titles)
        {
            @Override
            public View getView (int position, View convertView, ViewGroup parent)
            {
                TextView textView = (TextView)super.getView(position, convertView, parent);
                textView.setTextColor(Color.BLACK);

                return textView;
            }
        };
        // puts the array adapter into the list view
        ItemList.setAdapter(ItemListAdapter);

        search = (EditText)findViewById(R.id.editTextSearch);

        search.addTextChangedListener(new TextWatcher() {

            @Override
            public void onTextChanged(CharSequence cs, int start, int before, int count) {
                // Changes order of text in listBox
                AllItems.this.ItemListAdapter.getFilter().filter(cs);
            }

            // unused method
            @Override
            public void beforeTextChanged(CharSequence arg0, int arg1, int arg2,
                                          int arg3) {
                // TODO Auto-generated method stub
            }

            // unused method
            @Override
            public void afterTextChanged(Editable arg0) {
                // TODO Auto-generated method stub
            }
        });

        // makes the list view clickable
        ItemList.setOnItemClickListener(new NavigationListClickHandler());

        // The New Item button. It will take you to the screen  to take the images and set all the details
        ImageView newItem = (ImageView)findViewById(R.id.imageViewNewItem);
        newItem.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent newActivity = new Intent(AllItems.this, NewItem.class);
                startActivity(newActivity);
            }
        });

        this.getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN);

        about = (ImageView)findViewById(R.id.imageViewInformation);
        about.setOnClickListener(new AboutIntent());

    }

    // Get an array of the data from the database
    public ArrayList<String> GetData()
    {
        ArrayList<String> items = new ArrayList<String>();
        // Select name from database
        String selectQuery = "SELECT itemName FROM tblPictures";// this will be changed to Item Name
        // execute the query
        final Cursor recordSet = PictureDB.rawQuery(selectQuery,null);

        // If record set has something in it then it moveToFirst will return true
        if (recordSet.moveToFirst()) {
                boolean hasNext = true;
                while (hasNext)
                {
                    items.add(recordSet.getString(0));
                    hasNext = recordSet.moveToNext();
                }
            }
        return items; // returns an array
    }

    // Class for clicking on the Item
    public class NavigationListClickHandler implements AdapterView.OnItemClickListener {
        @Override
        public void onItemClick(AdapterView<?> list, View view, int position, long id) {

            // Change ID because position of the array starts at 0 and the first entry in the database as ID = 1
            int databaseID = position + 1;

            String itemStuff = (String) list.getItemAtPosition(position);

            // Select details for the clicked Item
            String selectQuery = "SELECT itemName,picture, price, store, date FROM tblPictures WHERE itemName = '"+itemStuff+"'";

            // Call query in Database
            final Cursor recordSet = PictureDB.rawQuery(selectQuery,null);
            recordSet.moveToFirst();

            // Get the first and only item from the cursor and turn it into a string
            String itemName = recordSet.getString(0);
            String pictureFileName = recordSet.getString(1);
            String price = recordSet.getString(2);
            String store = recordSet.getString(3);
            String date = recordSet.getString(4);

            // prepare to start the Item Info Intent
            Intent newActivity = new Intent(AllItems.this, ItemInfo.class);

            // send the picture details through the intent
            newActivity.putExtra("Item Name",itemName);
            newActivity.putExtra("Picture file name",pictureFileName);
            newActivity.putExtra("Item Price",price);
            newActivity.putExtra("item Store",store);
            newActivity.putExtra("Date Item Saved",date);

            // Start the AllImages Intent
            startActivity(newActivity);
        }
    }

    public class AboutIntent implements View.OnClickListener
    {
        @Override
        public void onClick(View view)
        {
            Intent changeToAboutIntent = new Intent(AllItems.this, About.class);
            startActivity(changeToAboutIntent);
        }
    }
}



