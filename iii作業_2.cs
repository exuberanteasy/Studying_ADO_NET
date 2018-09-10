3. Connected 連線環境  =>  FrmConnected.cs  

// TreeView / ListView => County: => Hint提示
        private void button31_Click_1(object sender, EventArgs e)
        {
            // ListView
            for (int i = 0; i < 10; i++)
            {
                ListViewGroup lvGroup = this.listView3.Groups.Add(i.ToString(), "County " + i);
                //lvGroup.Items.Add
                for (int j = 1; j <= 5; j++)
                {
                    ListViewItem lvItem = listView3.Items.Add(j.ToString());
                    lvItem.Group = lvGroup;
                }
            }

            //------------------------
            //TreeView
            for (int i = 1; i <= 10; i++)
            {
                TreeNode x = this.treeView1.Nodes.Add(i.ToString());
                for (int j = 1; j <= 5; j++)
                {
                    x.Nodes.Add(j.ToString());
                }
            }
        }
